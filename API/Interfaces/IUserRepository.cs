using API.DTOs;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task AddStudentAsync(int userId);
        Task AddInstructorAsync(int userId);
        Task<IList<StudentDto>> GetAllStudents();
        Task<IList<InstructorDto>> GetAllInstructors();
        Task<StudentDto> GetStudentById(int id);
        Task<InstructorDto> GetInstructorById(int id);
        void DeleteUser(int id);
    }
}
