using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CourseDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<InstructorDto> Instructors { get; set; }
        public ICollection<SemesterDto> Semesters { get; set; }
        public ICollection<StudentDto> Students { get; set; }
        public ICollection<PerformanceIndicatorDto> PerformanceIndicators { get; set; }
    }
}
