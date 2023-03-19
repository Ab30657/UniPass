using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class GetCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Instructors { get; set; }
        public int SemesterId { get; set; }
    }
}
