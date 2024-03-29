namespace API.Models
{
    public class Takes
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public Semester Semester { get; set; }
        public ICollection<TakesCoursePI> TakesCoursePIs { get; set; }
    }
}
