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
            var students = _unitOfWork.CourseRepository.GetAllStudents().Result;
            List<StudentDto> studentsDTO = new List<StudentDto>();
            students.ToList().ForEach(x => studentsDTO.Add(new StudentDto(x)));

            return Ok(studentsDTO);
        }

        [HttpGet("GetAllInstructorUsers")]
        public async Task<ActionResult<List<InstructorDto>>> GetAllInstructorUsers()
        {
            var instructors = _unitOfWork.CourseRepository.GetAllInstructors().Result;
            List<InstructorDto> intructorsDTO = new List<InstructorDto>();
            instructors.ToList().ForEach(x => intructorsDTO.Add(new InstructorDto(x)));

            return Ok(intructorsDTO);
        }

        [HttpDelete("DeleteStudentUsers")]
        public async Task<ActionResult> DeleteStudentUsers(List<int> ids)
        {
            _unitOfWork.CourseRepository.DeleteStudentUser(ids);
            return Ok();
        }

        [HttpDelete("DeleteInstructorUsers")]
        public async Task<ActionResult> DeleteInstructorUsers(List<int> ids)
        {
            _unitOfWork.CourseRepository.DeleteInstructorUser(ids);
            return Ok();
        }
    }
}
