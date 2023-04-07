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
        Task CreateCourse(CreateCourseDto courseDto);
        Task<CourseDetailDto> GetCourseByIdWithInstructors(int id);
        Task<Course> GetCourseById(int id);
        Task<bool> InstructorExists(int id);
        Task UpdateGradeForStudentCourse(int courseId, int stundetId, int semesterId, int assignmentScore);
        Task<bool> StudentExists(int id);
        Task<bool> SemesterExists(int id);
        void EditCourse(CreateCourseDto courseDto);
        Task<IList<CourseDto>> GetCoursesByInstructorId(int id);
        Task<IList<CourseDto>> GetCoursesByStudentId(int id);
        Task<IList<StudentWithScoreDto>> GetStudentWithScoresAsync(int courseId, int semesterId);
        bool YouDontTeach(Instructor instructor, int courseId);
        void RegisterForCourse(RegisterCourseDto rcDto);
        Task<bool> CourseExistsById(int id);
        Task<bool> CourseExistsByTitle(string title);
        Task<bool> StudentAlreadyRegistered(int courseId, int studentId);
        Task<Course> GetCourseByIdWithCoursePI(int id);
        Task<List<StudentDto>> GetStudentsToACourse(int courseId, int semesterId);
        Task<List<StudentAssignmentGradesDto>> GetStudentGradesForAssignment(int assignmentId);
        Task<List<CourseDto>> GetAllCourses();
    }
}
