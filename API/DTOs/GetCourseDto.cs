using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class GetCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public InstructorDto Instructor { get; set; }
        public SemesterDto Semester { get; set; }
    }
}
