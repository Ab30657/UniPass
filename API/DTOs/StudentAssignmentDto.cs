namespace API.DTOs
{
    public class StudentAssignmentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public ICollection<StudentQuestionDto> Questions { get; set; }
        public ICollection<TakeAssignmentDto> TakeAssignments { get; set; }
    }
}
