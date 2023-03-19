namespace API.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int FullMarks { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public ICollection<TakeQuestion> TakeQuestions { get; set; }
        public ICollection<QuestionPI> QuestionPIs { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
