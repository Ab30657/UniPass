namespace API.Models
{
    public class AssignmentPI
    {
        public Assignment Assignment { get; set; }
        public int AssignmentId { get; set; }
        public PerformanceIndicator PerformanceIndicator { get; set; }
        public int PerformanceIndicatorId { get; set; }
        public int FullScore { get; set; }
    }
}
