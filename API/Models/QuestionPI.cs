namespace API.Models
{
    public class QuestionPI
    {
        public int QuestionId { get; set; }
        public int PerformanceIndicatorId { get; set; }
        public Question Question { get; set; }
        public PerformanceIndicator PerformanceIndicator { get; set; }
    }
}