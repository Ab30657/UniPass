using API.Interfaces;
using API.Models;
using AutoMapper;

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
                var instructor = _context.Instructors.FirstOrDefault(x => x.Id == id);
                _context.Instructors.Remove(instructor);
                await _context.SaveChangesAsync();
            }
        }

        public async void DeleteStudentUser(List<int> ids)
        {
            foreach (var id in ids)
            {
                var student = _context.Students.FirstOrDefault(x => x.Id == id);
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Instructor>> GetAllInstructors()
        {
            return _context.Instructors.ToList();
        }

        public async Task<IList<Student>> GetAllStudents()
        {
            return _context.Students.ToList();
        }
    }
}
