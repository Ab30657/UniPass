namespace API.DTOs
{
    public class CreateAssignmentDto
    {
        public string Title { get; set; }
        public int SemesterId { get; set; }
        public ICollection<CreateQuestionDto> Questions { get; set; }
    }
}
