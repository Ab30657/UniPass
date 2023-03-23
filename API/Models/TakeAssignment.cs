namespace API.Models
{
    public class TakeAssignment
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int StudentId { get; set; }
        public int AssignmentId { get; set; }
        public Student Student { get; set; }
        public Assignment Assignment { get; set; }
        public ICollection<TakeQuestion> TakeQuestions { get; set; }
        public ICollection<TakeAssignmentPIScore> PIScores { get; set; }
    }
}
