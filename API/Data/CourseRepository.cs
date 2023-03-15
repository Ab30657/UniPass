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

        public async void DeleteInstructorUser(List<int> ids)
        {
            foreach (var id in ids)
            {
                var instructor = await _context.Instructors.FirstOrDefaultAsync(x => x.Id == id);
                _context.Instructors.Remove(instructor);
            }
        }

        public async void DeleteStudentUser(List<int> ids)
        {
            foreach (var id in ids)
            {
                var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
                _context.Students.Remove(student);
            }
        }

        public async Task<IList<InstructorDto>> GetAllInstructors()
        {
            return await _context.Instructors.Include(x => x.AppUser).ProjectTo<InstructorDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        //return await _context.Users.Where(x => x.UserName == username).ProjectTo<MemberDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

        public async Task<IList<StudentDto>> GetAllStudents()
        {
            return await _context.Students.Include(x => x.AppUser).ProjectTo<StudentDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<InstructorDto> GetInstructorById(int id)
        {
            return await _context.Instructors.Where(y => y.Id == id).Include(x => x.AppUser).ProjectTo<InstructorDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<StudentDto> GetStudentById(int id)
        {
            return await _context.Students.Where(y => y.Id == id).Include(x => x.AppUser).ProjectTo<StudentDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }
    }
}
