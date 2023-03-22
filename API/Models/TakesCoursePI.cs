namespace API.Models
{
    public class TakesCoursePI
    {
        public int Score { get; set; }
        public int TakesId { get; set; }
        public Takes Takes { get; set; }
        public CoursePI CoursePI { get; set; }
        public int CoursePIId { get; set; }
    }
}
