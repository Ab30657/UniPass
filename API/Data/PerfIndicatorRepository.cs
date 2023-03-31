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

        public async Task<bool> CourseExistsAsync(int courseId, int piId)
        {
            return (await _context.CoursePIs.FirstOrDefaultAsync(x => x.CourseId == courseId && x.PerformanceIndicatorId == piId) != null);
        }

        public async Task<bool> CourseHasPIAsync(int piId, int courseId)
        {
            return await _context.CoursePIs.AnyAsync(x => x.PerformanceIndicatorId == piId && courseId == x.CourseId);
        }

        public async Task<IEnumerable<PerformanceIndicatorDto>> GetPerfIndicatorsAsync()
        {
            return await _context.PerformanceIndicators.ProjectTo<PerformanceIndicatorDto>(_mapper.ConfigurationProvider).ToListAsync();
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

        public async Task UpdateCoursePIFullMarksAsync(int courseId, Assignment assignment, int pid, int fullPoints)
        {
            var coursePI = await _context.CoursePIs.Where(x => x.PerformanceIndicatorId == pid && x.CourseId == courseId).FirstOrDefaultAsync();
            coursePI.PIFullMarks += fullPoints;
            var assignmentPI = new AssignmentPI
            {
                Assignment = assignment,
                PerformanceIndicatorId = pid,
                FullScore = fullPoints
            };
            _context.AssignmentPIs.AddAsync(assignmentPI);
        }

        //Make sure every assignment creation, updates fullscore Pi of CoursePI
        //and this function adds if there is no score present, otherwise it updates on that
        public async Task UpdateTakesCoursePIAsync(int courseId, int studentId, int semesterId, int piId, int piScore)
        {
            var coursePI = await _context.CoursePIs.Where(x => x.CourseId == courseId && x.PerformanceIndicatorId == piId).FirstOrDefaultAsync();
            var takes = await _context.Takes.Where(x => x.SemesterId == semesterId && x.CourseId == courseId && x.StudentId == studentId).FirstOrDefaultAsync();
            var takesCoursePI = await _context.TakesCoursePIs.Where(x => x.PerformanceIndicatorId == piId && x.TakesId == takes.Id).FirstOrDefaultAsync();
            if (takesCoursePI == null)
            {
                //Add
                var takeCoursePI = new TakesCoursePI
                {
                    TakesId = takes.Id,
                    PerformanceIndicatorId = piId,
                    Score = piScore
                };
                _context.TakesCoursePIs.AddAsync(takeCoursePI);
            }
            else
            {
                //Update
                takesCoursePI.Score += piScore;
                _context.Entry<TakesCoursePI>(takesCoursePI).State = EntityState.Modified;
            }
        }

    }
}
