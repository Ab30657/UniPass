namespace API.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Teaches> Teaches { get; set; }
        public ICollection<Takes> Takes { get; set; }
        public ICollection<CoursePI> CoursePIs { get; set; }
        public ICollection<Assignment> Assignments { get; set; }

    }
}