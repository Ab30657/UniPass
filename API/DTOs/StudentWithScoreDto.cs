namespace API.DTOs
{
    public class StudentWithScoreDto
    {
        public StudentDto Student { get; set; }
        public int Grade { get; set; }
        public int FullMarks { get; set; }
        public ICollection<PIScoreDto> PerformanceIndicatorScores { get; set; }
    }
}
