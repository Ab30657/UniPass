using API.DTOs;

namespace API.Interfaces
{
    public interface ICourseRepository
    {
        void CreateCourse(CourseDto courseDto);
    }
}
