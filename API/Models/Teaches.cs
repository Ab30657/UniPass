namespace API.Models
{
    public class Teaches
    {
        public int InstructorId { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
        public Semester Semester { get; set; }
    }
}
