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
        public async Task<ActionResult<IEnumerable<GetCourseDto>>> GetCourses()
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
        public async Task<ActionResult<InstructorAssignmentDto>> GetClassMaterials(int courseId)
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

        [HttpPost("Courses/{courseId}/AddPI")]
        public async Task<ActionResult> AddPerformanceIndicicatorToCourse(int courseId, PerformanceIndicatorDto piDTO)
        {
            if (!(await _unitOfWork.CourseRepository.CourseExistsById(courseId)))
            {
                return BadRequest("Course does not exist.");
            }
            _unitOfWork.CourseRepository.AddPerformanceIndicatorToCourse(courseId, piDTO);
            return Ok("Added successfully");
        }

        [HttpDelete("Courses/{courseId}/DeletePI")]
        public async Task<ActionResult> DeletePerformanceIndicicatorFromCourse(CoursePIDto cpiDTO)
        {
            if (!(await _unitOfWork.CourseRepository.CourseExistsById(cpiDTO.CourseId)))
            {
                return BadRequest("Course does not exist.");
            }
            _unitOfWork.CourseRepository.DeletePerformanceIndicatorFromCourse(cpiDTO);
            return Ok("Deleted successfully");
        }
    }
}
