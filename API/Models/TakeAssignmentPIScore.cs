namespace API.Models
{
    public class TakeAssignmentPIScore
    {
        public PerformanceIndicator PerformanceIndicator { get; set; }
        public int PerformanceIndicatorId { get; set; }
        public TakeAssignment TakeAssignment { get; set; }
        public int TakeAssignmentId { get; set; }
        public int Score { get; set; }
    }
}
