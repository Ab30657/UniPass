using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PerfIndicatorRepository : IPerfIndicatorRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public PerfIndicatorRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async void AddPerfIndicator(PerformanceIndicator performanceIndicator)
        {
            await _context.PerformanceIndicators.AddAsync(performanceIndicator);
        }

        public async Task<IEnumerable<PerfIndicatorDto>> GetPerfIndicatorsAsync()
        {
            return await _context.PerformanceIndicators.ProjectTo<PerfIndicatorDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<PerformanceIndicator> GetPerformanceIndicatorByIdAsync(int id)
        {
            return await _context.PerformanceIndicators.Where(x => x.Id == id).SingleOrDefaultAsync();
        }
        public void Update(PerformanceIndicator perfIndicator)
        {
            //Not sure if we want to implement leave it for later.
            _context.Entry(perfIndicator).State = EntityState.Modified;
        }
    }
}
