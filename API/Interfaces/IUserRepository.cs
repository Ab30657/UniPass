using API.DTOs;
using API.Models;

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
        // Task<AppUser> GetUserById(int id);
        //might need later
        Task<Instructor> GetInstructorByUserIdAsync(int id);
        Task<Student> GetStudentByUserIdAsync(int id);
        Task<AppUser> GetUserByUsername(string username);
        void DeleteUser(int id);
    }
}
