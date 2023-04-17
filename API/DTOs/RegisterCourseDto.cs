using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterCourseDto
    {
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
    }
}
