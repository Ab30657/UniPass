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

        [HttpPost("CreateCourseEntity")]
        public async Task<ActionResult> CreateCourseEntity(CourseDto courseDto)
        {
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
        public async Task<ActionResult> GetCourseById(int id)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseById(id);
            return Ok(course);
        }

    }
}
