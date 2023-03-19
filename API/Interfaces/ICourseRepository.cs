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
        Task<InstructorDto> GetInstructorById(int id);
        void CreateCourse(CourseDto courseDto);
        Task<GetCourseDto> GetCourseByIdWithInstructors(int id);
        Task<CourseDto> GetCourseById(int id);
        Task<bool> InstructorExists(int id);
        Task<bool> StudentExists(int id);
        Task<bool> SemesterExists(int id);
        void EditCourse(CourseDto courseDto);
    }
}
