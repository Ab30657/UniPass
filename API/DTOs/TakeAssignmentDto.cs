namespace API.DTOs
{
    public class TakeAssignmentDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int StudentId { get; set; }
        public ICollection<TakeQuestionWithAnswerDto> TakeQuestions { get; set; }
    }
}
