using API.Models;

namespace API.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AppUserId { get; set; }
        public int Grade { get; set; }
    }
}
