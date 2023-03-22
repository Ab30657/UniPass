using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
        public async Task<ActionResult<IEnumerable<GetCourseDto>>> GetCourses()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
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
            var x = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(x ?? "1");
            var student = await _unitOfWork.UserRepository.GetStudentByUserIdAsync(userId);
            var assignment = await _unitOfWork.AssignmentRepository.GetAssignmentByIdAsync(assignmentId);
            if (assignment == null)
                return BadRequest();
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
                var piScore = new PIScore
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
    }
}
