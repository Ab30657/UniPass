namespace API.DTOs
{
    public class StudentAssignmentGradesDto
    {
        public StudentDto Student { get; set; }
        public int Grade { get; set; }
        public int FullMarks { get; set; }
        public ICollection<PIScoreDto> PerformanceIndicatorScores { get; set; }
        public List<TakeQuestionWithAnswerDto> TakeQuestions { get; set; }
    }
}
