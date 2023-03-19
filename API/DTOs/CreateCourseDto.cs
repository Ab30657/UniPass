using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateCourseDto
    {
        public string Title { get; set; }
        public int InstructorId { get; set; }
        public int SemesterId { get; set; }
    }
}
