namespace API.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public bool Correct { get; set; }
        public int AssignmentId { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public Assignment Assignment { get; set; }
        public ICollection<TakeQuestion> TakeQuestions { get; set; }
    }
}