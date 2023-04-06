namespace API.DTOs
{
    public class AssignmentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
    }
}
