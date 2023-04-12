namespace API.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<InstructorDto> Instructors { get; set; }
    }
}
