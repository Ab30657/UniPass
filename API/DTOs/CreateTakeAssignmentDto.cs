namespace API.DTOs
{
    public class CreateTakeAssignmentDto
    {
        public ICollection<CreateTakeQuestionDto> TakeQuestions { get; set; }
    }
}
