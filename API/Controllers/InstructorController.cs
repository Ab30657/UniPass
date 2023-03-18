using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class InstructorController : BaseApiController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public InstructorController(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        // Your Actions here //
        // have consequences //
        [HttpGet("Courses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var instructor = await _unitOfWork.UserRepository.GetInstructorByUserIdAsync(userId);
            var courses = await _unitOfWork.CourseRepository.GetCoursesByInstructorId(instructor.Id);
            return Ok(courses);
        }

        [HttpGet("Courses/{courseId}/Materials")]
        public async Task<ActionResult<InstructorAssignmentDto>> GetClassMaterials(int courseId)
        {
            // var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            // var instructor = await _unitOfWork.UserRepository.GetInstructorByUserIdAsync(userId);
            return Ok(await _unitOfWork.AssignmentRepository.GetAssignmentsByCourseIdAsync(courseId));
        }

        [HttpPost("Courses/{courseId}/Materials")]
        public async Task<ActionResult> CreateAssignment([FromBody] CreateAssignmentDto createAssignmentDto, int courseId)
        {
            var assignment = _mapper.Map<Assignment>(createAssignmentDto);
            assignment.CourseId = courseId;
            //Assignment is ready

            //Now start inserting questions
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
            }
            await _unitOfWork.AssignmentRepository.AddAssignmentAsync(assignment);
            if (await _unitOfWork.CompleteAsync())
                return Ok("Successfully created");
            return BadRequest("Failed to create");

        }

    }
}
