using API.Models;

namespace API.Interfaces
{
    public interface ICourseRepository
    {
        Task<IList<Student>> GetAllStudents();
        Task<IList<Instructor>> GetAllInstructors();
        void DeleteInstructorUser(List<int> ids);
        void DeleteStudentUser(List<int> ids);
    }
}
