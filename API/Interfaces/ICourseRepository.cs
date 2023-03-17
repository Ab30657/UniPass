using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface ICourseRepository
    {
        void CreateCourse(CourseDto courseDto);
        Task<Course> GetCourseById(int id);
        void EditCourse(Course course);
    }
}
