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

        [HttpGet("GetAllStudentUsers")]
        public async Task<ActionResult<List<StudentDto>>> GetAllStudentUsers()
        {
            var students = await _unitOfWork.CourseRepository.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("GetAllInstructorUsers")]
        public async Task<ActionResult<List<InstructorDto>>> GetAllInstructorUsers()
        {
            var instructors = await _unitOfWork.CourseRepository.GetAllInstructors();
            return Ok(instructors);
        }

        [HttpGet("GetStudentById")]
        public async Task<ActionResult<StudentDto>> GetStudentById(int id)
        {
            var student = await _unitOfWork.CourseRepository.GetStudentById(id);
            return Ok(student);
        }

        [HttpGet("GetInstructorById")]
        public async Task<ActionResult<InstructorDto>> GetInstructorById(int id)
        {
            var instructor = await _unitOfWork.CourseRepository.GetInstructorById(id);
            return Ok(instructor);
        }

        [HttpDelete("DeleteStudentUsers")]
        public async Task<ActionResult> DeleteStudentUsers(List<int> ids)
        {
            _unitOfWork.CourseRepository.DeleteStudentUser(ids);
            var result = await _unitOfWork.CompleteAsync();
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("DeleteInstructorUsers")]
        public async Task<ActionResult> DeleteInstructorUsers(List<int> ids)
        {
            _unitOfWork.CourseRepository.DeleteInstructorUser(ids);
            var result = await _unitOfWork.CompleteAsync();
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
