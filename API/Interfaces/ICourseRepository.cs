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
        void CreateCourse(CreateCourseDto courseDto);
        Task<GetCourseDto> GetCourseByIdWithInstructors(int id);
        Task<Course> GetCourseById(int id);
        Task<bool> InstructorExists(int id);
        Task UpdateGradeForStudentCourse(int courseId, int stundetId, int semesterId, int assignmentScore);
        Task<bool> StudentExists(int id);
        Task<bool> SemesterExists(int id);
        void EditCourse(CreateCourseDto courseDto);
        Task<IList<GetCourseDto>> GetCoursesByInstructorId(int id);
        Task<IList<GetCourseDto>> GetCoursesByStudentId(int id);
        Task<IList<StudentWithScoreDto>> GetStudentWithScoresAsync(int courseId, int semesterId);
        bool YouDontTeach(Instructor instructor, int courseId);
    }
}
