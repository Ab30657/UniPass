namespace API.DTOs
{
    public class StudentAssignmentGradesWithoutQuestionsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public StudentDto Student { get; set; }
        public int Grade { get; set; }
        public int FullMarks { get; set; }
        public ICollection<PIScoreDto> PerformanceIndicatorScores { get; set; }
    }
}
