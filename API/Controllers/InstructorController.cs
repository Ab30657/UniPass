using System.Security.Claims;
using API.Interfaces;
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
        public async Task<ActionResult<CourseDto>> GetCourses()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var instructor = await _unitOfWork.UserRepository.GetInstructorByUserIdAsync(userId);
            var courses = await _unitOfWork.CourseRepository.GetCoursesByInstructorId(instructor.Id);
            return Ok(courses);
        }

    }
}
