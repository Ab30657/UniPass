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

        //This should be similar to GetCoursesByStudentId, probably incorporate semester id later
        //** SEMESTER ID ** FEATURE IMPORTANT OR MAYBE JUST DON"T SHOW IT 
        public async Task<IList<GetCourseDto>> GetCoursesByInstructorId(int id)
        {
            return await _context.Courses.Where(x => x.Teaches.Any(x => x.InstructorId == id)).ProjectTo<GetCourseDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IList<GetCourseDto>> GetCoursesByStudentId(int id)
        {
            //open to change later
            //Takes is now available
            return await _context.Courses.Where(x => x.Takes.Any(x => x.StudentId == id)).ProjectTo<GetCourseDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task UpdateGradeForStudentCourse(int courseId, int studentId, int semesterId, int newAssignmentScore)
        {
            var takes = await _context.Takes.Where(x => x.CourseId == courseId && x.StudentId == studentId && x.SemesterId == semesterId).FirstOrDefaultAsync();
            var sum = await _context.Assignments.Where(x => x.CourseId == courseId && x.SemesterId == semesterId).SumAsync(x => x.FullMarks);
            takes.Grade += newAssignmentScore;
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

        public async void CreateCourse(CreateCourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            var instructor = await _context.Instructors.Include(x => x.Teaches).FirstOrDefaultAsync(x => x.Id == courseDto.InstructorId);
            var teach = new Teaches
            {
                InstructorId = courseDto.InstructorId,
                SemesterId = courseDto.SemesterId,
                Course = course
            };
            await _context.Courses.AddAsync(course);
            instructor.Teaches.Add(teach);
        }

        public async Task<Course> GetCourseById(int id)
        {
            return await _context.Courses.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<GetCourseDto> GetCourseByIdWithInstructors(int id)
        {
            return await _context.Courses.Where(x => x.Id == id).ProjectTo<GetCourseDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            // .Include(x => x.Teaches)
            // .ThenInclude(x => (x.Semester))
            // .ThenInclude(x => x.Instructor).ThenInclude(x => x.AppUser).FirstOrDefaultAsync();
        }

        public void EditCourse(CreateCourseDto courseDto)
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

        public bool YouDontTeach(Instructor instructor, int courseId)
        {
            return instructor.Teaches.Where(x => x.InstructorId == instructor.Id && x.CourseId == courseId).FirstOrDefault() == null;
        }

        public async Task<IList<StudentWithScoreDto>> GetStudentWithScoresAsync(int courseId, int semesterId)
        {
            var take = _context.Takes.Where(x => x.CourseId == courseId && x.SemesterId == semesterId).Include(x => x.TakesCoursePIs).ThenInclude(x => x.PerformanceIndicator).ToListAsync();
            return await _context.Takes.Where(x => x.CourseId == courseId && x.SemesterId == semesterId).ProjectTo<StudentWithScoreDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> CourseExistsById(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CourseExistsByTitle(string title)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Title == title);
            if (course == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> StudentAlreadyRegistered(int courseId, int studentId)
        {
            var student = await _context.Students.Include(w => w.Takes).FirstOrDefaultAsync(x => x.Id == studentId);

            //Change this later when mergine feature/take-assignment-grade
            var course = student.Takes.FirstOrDefault(y => y.CourseId == courseId);

            if (course != null)
            {
                return true;
            }
            return false;
        }

        public async void RegisterForCourse(RegisterCourseDto rcDto)
        {
            //var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
            var student = await _context.Students.Include(x => x.Takes).FirstOrDefaultAsync(x => x.Id == rcDto.StudentId);

            var taking = new Takes
            {
                StudentId = rcDto.StudentId,
                CourseId = rcDto.CourseId,
                SemesterId = rcDto.SemesterId
            };
            //Change this as well after feature/take-assignment-grade
            student.Takes.Add(taking);
        }

        //create TakesDto and Project the list to it before returning
        public async Task<List<Takes>> GetStudentsToACourse(int courseId)
        {
            var course = await _context.Courses.Include(x => x.Takes).FirstOrDefaultAsync(y => y.Id == courseId);
            return (course.Takes.ToList());
        }
    }
}
