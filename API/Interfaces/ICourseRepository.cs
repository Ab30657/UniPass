using API.DTOs;
using API.Models;

using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface ICourseRepository
    {
        Task<IList<StudentDto>> GetAllStudents();
        Task<IList<InstructorDto>> GetAllInstructors();
        Task<StudentDto> GetStudentById(int id);
        Task<InstructorDto> GetInstructorById(int id); void CreateCourse(CourseDto courseDto);
        Task<Course> GetCourseById(int id);
        void EditCourse(Course course);
    }
}
