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
        public async Task<ActionResult<StudentAssignmentDto>> GetClassMaterials(int courseId)
        {
            return Ok(await _unitOfWork.AssignmentRepository.GetAssignmentsForStudentAsync(courseId));
        }

        [HttpPost("Courses/Register/{courseId}")]
        public async Task<ActionResult> RegisterForCourse(RegisterCourseDto rcDto)
        {
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
