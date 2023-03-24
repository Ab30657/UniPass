namespace API.Models
{
    public class CoursePI
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int PerformanceIndicatorId { get; set; }
        public int FullPIScore { get; set; }
        public Course Course { get; set; }
        public PerformanceIndicator PerformanceIndicator { get; set; }
    }
}
