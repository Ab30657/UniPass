using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // [Authorize(Roles = "Instructor")]
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
        [HttpGet("Courses")]
        public async Task<ActionResult<IEnumerable<GetCourseDto>>> GetCourses()
        {
            var x = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
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
        [HttpGet("Courses/{courseId}/Materials")]
        public async Task<ActionResult<InstructorAssignmentDto>> GetClassMaterials(int courseId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var instructor = await _unitOfWork.UserRepository.GetInstructorByUserIdAsync(userId);
            if (_unitOfWork.CourseRepository.DoYouTeach(instructor, courseId))
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
