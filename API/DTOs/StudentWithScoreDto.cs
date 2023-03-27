namespace API.DTOs
{
    public class StudentWithScoreDto
    {
        public StudentDto Student { get; set; }
        public int TotalScore { get; set; } //Use this
        public ICollection<PIScoreDto> PerformanceIndicatorScores { get; set; }
    }
}
