namespace API.Models
{
    public class PerformanceIndicator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CoursePI> CoursePIs { get; set; }
        public ICollection<QuestionPI> QuestionPIs { get; set; }
    }
}