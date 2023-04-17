using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public InstructorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        // Your Actions here //
        // have consequences //

        [HttpGet("Courses/{courseId}/StudentReports")]
        public async Task<ActionResult<IEnumerable<StudentWithScoreDto>>> GetStudentsWithScore(int courseId, int semesterId)
        {

            return Ok(await _unitOfWork.CourseRepository.GetStudentWithScoresAsync(courseId, semesterId));
        }

        [HttpGet("Courses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            //This gets the currently logged in user claims from .NET Web API Middleware through HttpContext
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // Uncomment the line below, to test manually, otherwise use postman tests
            // var userId = int.Parse(x ?? "1");
            // var x = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // var userId = int.Parse(x ?? "2");
            var instructor = await _unitOfWork.UserRepository.GetInstructorByUserIdAsync(userId);
            var courses = await _unitOfWork.CourseRepository.GetCoursesByInstructorId(instructor.Id);
            return Ok(courses);
        }

        [HttpGet("Courses/{courseId}")]
        public async Task<ActionResult> GetCourse(int courseId)
        {
            //add
            var course = await _unitOfWork.CourseRepository.GetCourseByIdWithInstructors(courseId);
            return Ok(course);
        }

        //Might need to modify this to only include data on the assignment, and not really the depth for questions, answers, and rest of the stuff
        [HttpGet("Courses/{courseId}/Materials")]
        public async Task<ActionResult<AssignmentDto>> GetClassMaterials(int courseId)
        {
            //This gets the currently logged in user claims from .NET Web API Middleware through HttpContext
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // Uncomment the line below, to test manually, otherwise use postman tests
            // var userId = int.Parse(x ?? "1");
            // var x = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // var userId = int.Parse(x ?? "2");
            var instructor = await _unitOfWork.UserRepository.GetInstructorByUserIdAsync(userId);
            if (_unitOfWork.CourseRepository.YouDontTeach(instructor, courseId))
                return BadRequest("You don't teach this course");
            return Ok(await _unitOfWork.AssignmentRepository.GetAssignmentsByCourseIdAsync(courseId));
        }

        [HttpPost("Courses/{courseId}/Materials")]
        public async Task<ActionResult> CreateAssignment([FromBody] CreateAssignmentDto createAssignmentDto, int courseId)
        {
            var assignment = _mapper.Map<Assignment>(createAssignmentDto);
            assignment.CourseId = courseId;
            //Assignment is ready

            //Now start inserting questions
            int fullMarks = 0;
            Dictionary<int, int> dt = new Dictionary<int, int>(); //piid, fullmarks
            foreach (var question in assignment.Questions)
            {
                question.Assignment = assignment;
                await _unitOfWork.AssignmentRepository.AddQuestionAsync(question);
                foreach (var answer in question.Answers)
                {
                    answer.Assignment = assignment;
                    answer.Question = question;
                    await _unitOfWork.AssignmentRepository.AddAnswerAsync(answer);
                }
                fullMarks += question.FullMarks;
                foreach (var pi in question.QuestionPIs)
                {
                    if (!(await _unitOfWork.PerfIndicatorRepository.CourseHasPIAsync(pi.PerformanceIndicatorId, courseId)))
                        return BadRequest("Invalid performance indicator involved.");
                    if (!dt.ContainsKey(pi.PerformanceIndicatorId))
                        dt.Add(pi.PerformanceIndicatorId, 0);
                    dt[pi.PerformanceIndicatorId] += question.FullMarks;
                }
            }
            assignment.FullMarks = fullMarks;
            foreach (var key in dt.Keys)
            {
                //add to coursePI the fullmarks for that PI
                //later when compute score for TakesPIScore, this full marks is needed
                //This also updates Assignment PI
                await _unitOfWork.PerfIndicatorRepository.UpdateCoursePIFullMarksAsync(courseId, assignment, key, dt[key]);
            }
            await _unitOfWork.AssignmentRepository.AddAssignmentAsync(assignment);
            if (await _unitOfWork.CompleteAsync())
                return Ok("Successfully created");
            return BadRequest("Failed to create");

        }


        //Later add functionality to change title, description
        //Have a courseUpdateDto or similar
        [HttpPut("Course/{courseId}")]
        public async Task<IActionResult> UpdateCoursePerformanceIndicators(int courseId, [FromBody] List<int> performanceIndicatorIds)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseByIdWithCoursePI(courseId);

            if (course == null)
            {
                return NotFound($"Course with ID {courseId} not found.");
            }

            // Get the existing performance indicators for the course
            var existingPerformanceIndicators = course.CoursePIs.ToList();

            // Add new performance indicators to the course
            var newPerformanceIndicators = performanceIndicatorIds.Except(existingPerformanceIndicators.Select(pi => pi.Id)).ToList();
            foreach (var perfIndicatorId in newPerformanceIndicators)
            {
                var perfIndicator = await _unitOfWork.PerfIndicatorRepository.GetPerformanceIndicatorByIdAsync(perfIndicatorId);
                if (perfIndicator != null)
                {
                    var cpi = new CoursePI
                    {
                        CourseId = courseId,
                        PerformanceIndicatorId = perfIndicatorId
                    };

                    course.CoursePIs.Add(cpi);
                }
            }

            // Remove existing performance indicators from the course
            var removedPerformanceIndicators = existingPerformanceIndicators.Where(pi => !performanceIndicatorIds.Contains(pi.Id));
            foreach (var perfIndicator in removedPerformanceIndicators)
            {
                course.CoursePIs.Remove(perfIndicator);
            }

            // Save changes to the database
            if (await _unitOfWork.CompleteAsync())
            {
                return Ok("Updated successfully!");
            }
            else
            {
                return StatusCode(500, "An error occurred while saving changes to the database.");
            }
        }

        [HttpGet("Course/{courseId}/students")]
        public async Task<ActionResult<List<StudentDto>>> GetStudentsToACourse(int courseId, int semesterId)
        {
            var courseExists = await _unitOfWork.CourseRepository.CourseExistsById(courseId);
            if (!courseExists)
            {
                return NotFound("Course does not exist.");
            }
            var students = await _unitOfWork.CourseRepository.GetStudentsToACourse(courseId, semesterId);
            return Ok(students);
        }

        [HttpGet("Assignment/{assignmentId}/grades")]
        public async Task<ActionResult<List<StudentAssignmentGradesDto>>> GetStudentGrades(int assignmentId)
        {
            var studentGrades = await _unitOfWork.CourseRepository.GetStudentGradesForAssignment(assignmentId);
            return Ok(studentGrades);
        }

        [HttpGet("Assignment/{assignmentId}/{studentId}/grades")]
        public async Task<ActionResult<List<StudentAssignmentGradesDto>>> GetStudentGradeForAssignmentById(int studentId, int assignmentId)
        {
            var studentGrade = await _unitOfWork.CourseRepository.GetStudentGradeForAssignmentById(studentId, assignmentId);
            return Ok(studentGrade);
        }

        [HttpGet("PI")]
        public async Task<ActionResult<IEnumerable<PerformanceIndicatorDto>>> GetAllIndicators()
        {
            return Ok(await _unitOfWork.PerfIndicatorRepository.GetPerfIndicatorsAsync());
        }

        [HttpGet("Courses/{courseId}/PI")]
        public async Task<ActionResult<IEnumerable<PerformanceIndicatorDto>>> GetCourseIndicator(int courseId)
        {
            return Ok(await _unitOfWork.PerfIndicatorRepository.GetPerfIndicatorsForCourse(courseId));
        }
    }
}
