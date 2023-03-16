using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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
            await _context.Courses.AddAsync(course);
        }
    }
}
