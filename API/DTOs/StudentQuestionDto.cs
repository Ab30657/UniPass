namespace API.DTOs
{
    public class StudentQuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int FullMarks { get; set; }
        public ICollection<PerformanceIndicatorDto> PerformanceIndicators { get; set; }
        public ICollection<StudentAnswerDto> Answers { get; set; }
    }
}
