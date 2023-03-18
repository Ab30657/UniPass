using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int InstructorId { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
    }
}
