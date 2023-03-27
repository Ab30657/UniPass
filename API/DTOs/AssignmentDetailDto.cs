namespace API.DTOs
{
    public class AssignmentDetailDto<T> where T : StudentQuestionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public ICollection<T> Questions { get; set; }
        public ICollection<TakeAssignmentDto> TakeAssignments { get; set; }
    }
}
