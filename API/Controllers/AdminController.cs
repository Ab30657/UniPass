using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
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
    }
}
