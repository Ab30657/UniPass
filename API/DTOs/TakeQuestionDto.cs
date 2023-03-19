namespace API.DTOs
{
    public class TakeQuestionDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
        public bool Correct { get; set; }
    }
}
