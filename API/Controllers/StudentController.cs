using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : BaseApiController
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public StudentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        // Your Actions here //
        // have consequences //

        [HttpGet("Courses")]
        public async Task<ActionResult<IEnumerable<GetCourseDto>>> GetCoursesForStudent()
        {
            //This gets the currently logged in user claims from .NET Web API Middleware through HttpContext
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // Uncomment the line below, to test manually, otherwise use postman tests
            // var userId = int.Parse(x ?? "1");
            var student = await _unitOfWork.UserRepository.GetStudentByUserIdAsync(userId);
            var courses = await _unitOfWork.CourseRepository.GetCoursesByStudentId(student.Id);
            return Ok(courses);
        }

        [HttpGet("Courses/{courseId}")]
        public async Task<ActionResult> GetCourse(int courseId)
        {
            //add
            var course = await _unitOfWork.CourseRepository.GetCourseByIdWithInstructors(courseId);
            return Ok(course);
        }

        [HttpGet("Courses/{courseId}/Materials")]
        public async Task<ActionResult<IList<StudentAssignmentDto>>> GetClassMaterials(int courseId)
        {
            return Ok(await _unitOfWork.AssignmentRepository.GetAssignmentsForStudentAsync(courseId));
        }

        [HttpGet("Courses/{courseId}/Materials/{assignmentId}")]
        public async Task<ActionResult<StudentAssignmentDto>> GetClassMaterial(int courseId, int assignmentId)
        {
            //courseId is to check if student is enrolled in the course
            return Ok(await _unitOfWork.AssignmentRepository.GetAssignmentByIdAsync(assignmentId));
        }

        [HttpPost("Courses/{courseId}/Materials/{assignmentId}")]
        public async Task<ActionResult<AssignmentAttemptGradeDto>> SubmitAssignment(int courseId, int assignmentId, CreateTakeAssignmentDto createTakeAssignmentDto)
        {
            //This gets the currently logged in user claims from .NET Web API Middleware through HttpContext
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // Uncomment the line below, to test manually, otherwise use postman tests
            // var userId = int.Parse(x ?? "1");
            var student = await _unitOfWork.UserRepository.GetStudentByUserIdAsync(userId);
            var assignment = await _unitOfWork.AssignmentRepository.GetAssignmentByIdAsync(assignmentId);
            if (assignment == null)
                return BadRequest("No such assignment");
            var oldAssignmentTake = await _unitOfWork.AssignmentRepository.GetAssignmentAttemptByStudentIdAsync(student.Id, assignmentId);
            if (oldAssignmentTake != null)
                return BadRequest("You've already submitted the assignment");
            //courseId is to check if student is enrolled
            var assignmentTake = _mapper.Map<TakeAssignment>(createTakeAssignmentDto);
            assignmentTake.AssignmentId = assignmentId;
            assignmentTake.StudentId = student.Id;
            int score = 0;

            Dictionary<int, int> PIScores = new Dictionary<int, int>(); //PID, score
            foreach (var takequestion in assignmentTake.TakeQuestions)
            {
                takequestion.TakeAssignment = assignmentTake;
                await _unitOfWork.AssignmentRepository.AddTakeQuestionAsync(takequestion);
                var question = (await _unitOfWork.AssignmentRepository.GetQuestionByIdAsync(takequestion.QuestionId));
                if (question == null)
                    return BadRequest("QuestionId is invalid");
                var questionPIs = question.QuestionPIs.Select(x => x.PerformanceIndicatorId).ToList();
                var attemptedAnswer = (await _unitOfWork.AssignmentRepository.GetAnswerByIdAsync(takequestion.AnswerId));
                if (attemptedAnswer.QuestionId != takequestion.QuestionId)
                    return BadRequest("Submission invalid");

                score += attemptedAnswer.Correct ? question.FullMarks : 0;
                foreach (var pi in questionPIs)
                {
                    if (!PIScores.ContainsKey(pi))
                        PIScores.Add(pi, 0);
                    PIScores[pi] = (attemptedAnswer.Correct ? (PIScores[pi] + question.FullMarks) : 0);
                }
            }
            foreach (var piId in PIScores.Keys)
            {
                var piScore = new TakeAssignmentPIScore
                {
                    PerformanceIndicatorId = piId,
                    Score = PIScores[piId],
                    TakeAssignment = assignmentTake
                };
                //Addperfindicator score for the assignment
                await _unitOfWork.AssignmentRepository.AddAssignmentPIScoreAsync(piScore);
                //Now update all the overall performance indicator scores in TakesCoursePI
                if (await _unitOfWork.PerfIndicatorRepository.CourseExistsAsync(courseId, piId) == false)
                    return BadRequest("Invalid request for course pi. CoursePI 404");
                await _unitOfWork.PerfIndicatorRepository.UpdateTakesCoursePIAsync(courseId, student.Id, assignment.SemesterId, piId, piScore.Score);

            }
            assignmentTake.Score = score;

            //Now update the overall grade for the student
            await _unitOfWork.CourseRepository.UpdateGradeForStudentCourse(courseId, student.Id, assignment.SemesterId, score);

            await _unitOfWork.AssignmentRepository.AddTakeAssignmentAsync(assignmentTake);
            if (await _unitOfWork.CompleteAsync())
            {
                var result = await _unitOfWork.AssignmentRepository.GetTakeAssignmentByIdAsync(assignmentTake.Id);
                return Ok(result);
            }
            return BadRequest("Failed to submit assignment");
        }

        [HttpPost("Courses/Register")]
        public async Task<ActionResult> RegisterForCourse(RegisterCourseDto rcDto)
        {
            //Change this later with feature/take-assignment-grade
            //We use the logged in student 
            //Dont let the client determine who the student is
            if (!(await _unitOfWork.CourseRepository.CourseExistsById(rcDto.CourseId)))
            {
                return BadRequest("Course does not exist.");
            }

            if (!(await _unitOfWork.CourseRepository.StudentExists(rcDto.StudentId)))
            {
                return BadRequest("Student does not exist.");
            }

            if (!(await _unitOfWork.CourseRepository.SemesterExists(rcDto.SemesterId)))
            {
                return BadRequest("Semester does not exist.");
            }

            if (await _unitOfWork.CourseRepository.StudentAlreadyRegistered(rcDto.CourseId, rcDto.StudentId))
            {
                return BadRequest("Student is already registerd.");
            }

            _unitOfWork.CourseRepository.RegisterForCourse(rcDto);

            var result = await _unitOfWork.CompleteAsync();
            if (result == false)
            {
                return BadRequest("Register unsuccessful.");
            }

            return Ok("Register successful.");
        }
    }
}
