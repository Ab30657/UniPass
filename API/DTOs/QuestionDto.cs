namespace API.DTOs
{
    public class InstructorQuestionDto : StudentQuestionDto
    {
        public ICollection<AnswerDto> Answers { get; set; }
    }
}
