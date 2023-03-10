namespace API.Models
{
    public class CoursePI
    {
        public int CourseId { get; set; }
        public int PerformanceIndicatorId { get; set; }
        public Course Course { get; set; }
        public PerformanceIndicator PerformanceIndicator { get; set; }
    }
}