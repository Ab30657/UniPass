namespace API.DTOs
{
    public class AssignmentAttemptGradeDto
    {

        public StudentDto Student { get; set; }
        public ICollection<PIScoreDto> PIScores { get; set; }
        public TakeAssignmentDto TakeAssignment { get; set; }
    }
}
