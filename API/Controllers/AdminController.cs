using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class AdminController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AdminController(IUnitOfWork unitOfWork, IMapper mapper)
        {
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

    }
}
