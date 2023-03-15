using API.Models;

namespace API.DTOs
{
    public class InstructorDto
    {
        public InstructorDto() { }

        public InstructorDto(Instructor x)
        {
            Id = x.Id;
            AppUserId = x.AppUserId;
            AppUser = x.AppUser;
        }

        public int Id { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}