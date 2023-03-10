namespace API.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public Course Course { get; set; }
        public Semester Semester { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<TakeAssignment> TakeAssignments { get; set; }
    }
}