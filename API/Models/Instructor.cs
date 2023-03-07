namespace API.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Teaches> Teaches { get; set; }
    }
}