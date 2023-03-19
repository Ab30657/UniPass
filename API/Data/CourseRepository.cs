using API.DTOs;
using API.DTOs;
using API.Interfaces;
using API.Models;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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

        //return await _context.Users.Where(x => x.UserName == username).ProjectTo<MemberDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

        public async void CreateCourse(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            var instructor = await _context.Instructors.FirstOrDefaultAsync(x => x.Id == courseDto.InstructorId);
            var semester = await _context.Semesters.FirstOrDefaultAsync(y => y.Id == courseDto.SemesterId);

            var teach = new Teaches
            {
                InstructorId = instructor.Id,
                SemesterId = semester.Id,
                CourseId = courseDto.Id,
                Semester = semester,
                Instructor = instructor,
                Course = course
            };

            _context.Attach(course);
            course.Teaches = new HashSet<Teaches>();
            course.Teaches.Add(teach);

            await _context.Courses.AddAsync(course);
        }

        public async void AddInstructorToCourse(TeachesDto teachesDto)
        {
            var teach = _mapper.Map<Teaches>(teachesDto);

            teach.Instructor = await _context.Instructors.FirstOrDefaultAsync(w => w.Id == teachesDto.InstructorId);

            teach.Semester = await _context.Semesters.FirstOrDefaultAsync(x => x.Id == teachesDto.SemesterId);

            var course = await _context.Courses.FirstOrDefaultAsync(y => y.Id == teachesDto.CourseId);
            teach.Course = course;

            _context.Attach(course);
            course.Teaches = new HashSet<Teaches>();
            course.Teaches.Add(teach);
        }

        public async Task<CourseDto> GetCourseById(int id)
        {
            return await _context.Courses.ProjectTo<CourseDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GetCourseDto> GetCourseByIdWithInstructors(int id)
        {
            return await _context.Courses.ProjectTo<GetCourseDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
        }

        public void EditCourse(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            _context.Entry(course).State = EntityState.Modified;
        }

        public async Task<bool> InstructorExists(int id)
        {
            var instructor = await _context.Instructors.FirstOrDefaultAsync(x => x.Id == id);
            if (instructor == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> StudentExists(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SemesterExists(int id)
        {
            var semester = await _context.Semesters.FirstOrDefaultAsync(x => x.Id == id);
            if (semester == null)
            {
                return false;
            }
            return true;
        }
    }
}
