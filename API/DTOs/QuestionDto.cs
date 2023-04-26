namespace API.DTOs
{
    public class InstructorQuestionDto : StudentQuestionDto
    {
        public ICollection<InstructorAnswerDto> Answers { get; set; }
    }
}
