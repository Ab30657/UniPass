using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Student Student { get; set; }
        public Instructor Instructor { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}