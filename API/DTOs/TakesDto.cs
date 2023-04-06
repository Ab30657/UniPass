using API.Models;

namespace API.DTOs
{
    public class TakesDto
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
    }
}
