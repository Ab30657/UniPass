namespace API.Models
{
    public class TakesCoursePI
    {
        public int Score { get; set; }
        public int TakesId { get; set; }
        public Takes Takes { get; set; }
        public PerformanceIndicator PerformanceIndicator { get; set; }
        public int PerformanceIndicatorId { get; set; }
    }
}
