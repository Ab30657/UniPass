using API.Data;
using API.DTOs;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class AdminController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public AdminController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;

        }

        [HttpPost("PI")]
        public async Task<ActionResult<PerfIndicatorDto>> AddIndicator([FromBody] string addPerfIndicatorDto)
        {
            if (String.IsNullOrEmpty(addPerfIndicatorDto)) return BadRequest("Performance Indicator cannot be null");
            var performanceIndicator = new PerformanceIndicator
            {
                Name = addPerfIndicatorDto
            };

            _unitOfWork.PerfIndicatorRepository.AddPerfIndicator(performanceIndicator);
            if (await _unitOfWork.CompleteAsync())
            {
                return Ok("Successfully created");
            }
            return BadRequest("New performance indicator failed to create");
        }

        [HttpPut("PI")]
        public async Task<ActionResult<PerfIndicatorDto>> UpdatePerformanceIndicator([FromBody] PerfIndicatorDto perfIndicatorDto)
        {
            var perfIndicator = await _unitOfWork.PerfIndicatorRepository.GetPerformanceIndicatorByIdAsync(perfIndicatorDto.Id);
            if (perfIndicator == null) return NotFound("Performance Indicator Not Found");
            _mapper.Map(perfIndicatorDto, perfIndicator);
            _unitOfWork.PerfIndicatorRepository.Update(perfIndicator);
            if (await _unitOfWork.CompleteAsync())
                return Ok("Updated succesfully");
            return BadRequest("Performance Indicator Update failed");
        }
        [HttpGet("PI/{id}")]
        public async Task<ActionResult<PerfIndicatorDto>> GetIndicator(int id)
        {
            var performanceIndicator = await _unitOfWork.PerfIndicatorRepository.GetPerformanceIndicatorByIdAsync(id);
            if (performanceIndicator == null) return BadRequest();
            return Ok(_mapper.Map<PerfIndicatorDto>(performanceIndicator));
        }

        [HttpGet("PI")]
        public async Task<ActionResult<IEnumerable<PerfIndicatorDto>>> GetAllIndicators()
        {
            return Ok(await _unitOfWork.PerfIndicatorRepository.GetPerfIndicatorsAsync());
        }
        // Your Actions here //
        // have consequences //

        //
        // @TODO: Later implement GetUserById()
        // 

        [HttpGet("Users")]
        public async Task<ActionResult> GetAll()
        {
            var users = await _userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).OrderBy(x => x.UserName).Select(x => new
            {
                x.Id,
                Username = x.UserName,
                Roles = x.UserRoles.Select(x => x.Role.Name).ToList()
            }).ToListAsync();
            return Ok(users);
        }

        [HttpGet("Students")]
        public async Task<ActionResult<List<StudentDto>>> GetAllStudentUsers()
        {
            var students = await _unitOfWork.UserRepository.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("Instructors")]
        public async Task<ActionResult<List<InstructorDto>>> GetAllInstructorUsers()
        {
            var instructors = await _unitOfWork.UserRepository.GetAllInstructors();
            return Ok(instructors);
        }

        //Keep it for now
        //Might be useful later
        //
        //Admin doesn't care about Student Id,Instructor Id
        //param is student id
        [HttpGet("Student/{id}")]
        public async Task<ActionResult<StudentDto>> GetStudentById(int id)
        {

            var student = await _unitOfWork.UserRepository.GetStudentById(id);
            return Ok(student);
        }

        //Same as GetStudentById situation
        [HttpGet("Instructor/{id}")]
        public async Task<ActionResult<InstructorDto>> GetInstructorById(int id)
        {
            var instructor = await _unitOfWork.UserRepository.GetInstructorById(id);
            return Ok(instructor);
        }

        [HttpDelete("Users/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            _unitOfWork.UserRepository.DeleteUser(id);
            var result = await _unitOfWork.CompleteAsync();
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        // [HttpDelete("Instructor/{id}")]
        // public async Task<ActionResult> DeleteInstructorUser(int id)
        // {
        //     _unitOfWork.UserRepository.DeleteInstructorUser(id);
        //     var result = await _unitOfWork.CompleteAsync();
        //     if (result == false)
        //     {
        //         return BadRequest();
        //     }
        //     return Ok();
        // }

        [HttpPost("CreateCourse")]
        public async Task<ActionResult> CreateCourseEntity([FromBody] CourseDto courseDto)
        {
            if (!(await _unitOfWork.CourseRepository.InstructorExists(courseDto.InstructorId)))
            {
                return BadRequest("Instructor does not exist.");
            }

            if (!(await _unitOfWork.CourseRepository.SemesterExists(courseDto.SemesterId)))
            {
                return BadRequest("Semester does not exist.");
            }

            _unitOfWork.CourseRepository.CreateCourse(courseDto);
            var result = await _unitOfWork.CompleteAsync();

            if (result == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("Courses")]
        public async Task<ActionResult> EditCourse(UpdateCourseDto updateCourse)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseById(updateCourse.Id);
            _mapper.Map(updateCourse, course);
            _unitOfWork.CourseRepository.EditCourse(course);

            var result = await _unitOfWork.CompleteAsync();
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("Course/{id}")]
        public async Task<ActionResult<GetCourseDto>> GetCourseById(int id)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseByIdWithInstructors(id);
            return Ok(course);
        }

    }
}
