using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CourseRepository : ICourseRepository
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public CourseRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        //return await _context.Users.Where(x => x.UserName == username).ProjectTo<MemberDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

        public async void CreateCourse(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            var instructor = _context.Instructors.FirstOrDefaultAsync(x => x.Id == courseDto.InstructorId);
            var teaches = new Teaches
            {
                InstructorId = courseDto.InstructorId,
                CourseId = courseDto.CourseId,
                SemesterId = courseDto.SemesterId
            };

            _context.Attach(course);
            course.Teaches.Add(teaches);

            _context.Attach(instructor);
            course.Teaches.Add(teaches);

            await _context.Courses.AddAsync(course);
        }

        public async Task<Course> GetCourseById(int id)
        {
            return await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void EditCourse(Course course)
        {
            _context.Entry(course).State = EntityState.Modified;
        }
    }
}
