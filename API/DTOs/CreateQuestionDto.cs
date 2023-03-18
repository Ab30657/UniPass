namespace API.DTOs
{
    public class CreateQuestionDto
    {
        public string QuestionText { get; set; }
        public int FullMarks { get; set; }
        public ICollection<int> PerformanceIndicators { get; set; }
        public ICollection<CreateAnswerDto> Answers { get; set; }
    }
}
