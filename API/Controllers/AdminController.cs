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
        public AdminController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;

        }

        // Your Actions here //
        // have consequences //

        [HttpGet("Students")]
        public async Task<ActionResult<List<StudentDto>>> GetAllStudentUsers()
        {
            var students = await _unitOfWork.CourseRepository.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("Instructors")]
        public async Task<ActionResult<List<InstructorDto>>> GetAllInstructorUsers()
        {
            var instructors = await _unitOfWork.CourseRepository.GetAllInstructors();
            return Ok(instructors);
        }

        [HttpGet("Student/{id}")]
        public async Task<ActionResult<StudentDto>> GetStudentById(int id)
        {
            var student = await _unitOfWork.CourseRepository.GetStudentById(id);
            return Ok(student);
        }

        [HttpGet("Instructor/{id}")]
        public async Task<ActionResult<InstructorDto>> GetInstructorById(int id)
        {
            var instructor = await _unitOfWork.CourseRepository.GetInstructorById(id);
            return Ok(instructor);
        }

        [HttpDelete("Student/{id}")]
        public async Task<ActionResult> DeleteStudentUsers(int id)
        {
            _unitOfWork.CourseRepository.DeleteStudentUser(id);
            var result = await _unitOfWork.CompleteAsync();
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("Instructor/{id}")]
        public async Task<ActionResult> DeleteInstructorUsers(int id)
        {
            _unitOfWork.CourseRepository.DeleteInstructorUser(id);
            var result = await _unitOfWork.CompleteAsync();
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
