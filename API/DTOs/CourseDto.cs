namespace API.DTOs
{
    public class CourseDto
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<StudentDto> Students { get; set; }
        public ICollection<PerformanceIndicatorDto> PerformanceIndicators { get; set; }
    }
}
