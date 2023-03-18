using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
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
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var student = await _unitOfWork.UserRepository.GetStudentByUserIdAsync(userId);
            var courses = await _unitOfWork.CourseRepository.GetCoursesByStudentId(student.Id);
            return Ok(courses);
        }

        [HttpGet("Courses/{courseId}")]
        public async Task<ActionResult> GetCourse(int courseId)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseForStudentAsync(courseId);
            var result = _mapper.Map<CourseDto>(course);
            return Ok(result);
        }

        [HttpGet("Courses/{courseId}/Materials")]
        public async Task<ActionResult<StudentAssignmentDto>> GetClassMaterials(int courseId)
        {
            return Ok(await _unitOfWork.AssignmentRepository.GetAssignmentsForStudentAsync(courseId));
        }
    }
}
