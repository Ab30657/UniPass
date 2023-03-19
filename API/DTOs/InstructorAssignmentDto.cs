namespace API.DTOs
{
    public class InstructorAssignmentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }
        public ICollection<TakeAssignmentDto> TakeAssignments { get; set; }
    }
}
