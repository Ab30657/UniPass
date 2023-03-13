using System.Text.Json;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync())
            {
                return;
            }

            var users = JsonSerializer.Deserialize<List<AppUser>>(await System.IO.File.ReadAllTextAsync("Data/SeedData.json"));
            if (users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Student"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Instructor"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "12345");
                await userManager.AddToRoleAsync(user, "Student");
            }

            var admin = new AppUser
            {
                UserName = "admin",
                FirstName = "amar",
                LastName = "b",
                Email = "a@gmail.com",
            };

            await userManager.CreateAsync(admin, "12345");
            await userManager.AddToRolesAsync(admin, new[] { "Admin" });

        }
    }
}
