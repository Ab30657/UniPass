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

        //This should be similar to GetCoursesByStudentId
        public async Task<IList<CourseDto>> GetCoursesByInstructorId(int id)
        {
            return await _context.Courses.Where(x => x.Teaches.Any(x => x.InstructorId == id)).ProjectTo<CourseDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        //These might be useful later when we start on registering courses
        public async Task<IList<InstructorDto>> GetAllInstructors()
        {
            return await _context.Instructors.Include(x => x.AppUser).ProjectTo<InstructorDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        //return await _context.Users.Where(x => x.UserName == username).ProjectTo<MemberDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

        //These will be useful after modification for get students with courses and rest of that stuff
        public async Task<IList<StudentDto>> GetAllStudents()
        {
            return await _context.Students.Include(x => x.AppUser).ProjectTo<StudentDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
        //same for these
        public async Task<InstructorDto> GetInstructorById(int id)
        {
            return await _context.Instructors.Where(y => y.Id == id).Include(x => x.AppUser).ProjectTo<InstructorDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        //same for these
        public async Task<StudentDto> GetStudentById(int id)
        {
            return await _context.Students.Where(y => y.Id == id).Include(x => x.AppUser).ProjectTo<StudentDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }
    }
}
