using System.Text.Json;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, DataContext dataContext)
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

            for (int i = 0; i < users.Count; ++i)
            {
                var user = users[i];
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "12345");
                if (i % 2 == 0)
                {
                    await userManager.AddToRoleAsync(user, "Student");
                    await dataContext.Students.AddAsync(new Student { AppUserId = user.Id });
                }
                else
                {
                    await userManager.AddToRoleAsync(user, "Instructor");
                    await dataContext.Instructors.AddAsync(new Instructor { AppUserId = user.Id });
                }
            }
            dataContext.Semesters.AddAsync(new Semester { Season = "Fall", StartDate = default, EndDate = default });
            dataContext.PerformanceIndicators.AddRangeAsync(new PerformanceIndicator { Name = "Debugging" }, new PerformanceIndicator { Name = "Complexity Analysis" }, new PerformanceIndicator { Name = "System Design" });
            var course = new Course { Title = "CSC" };
            course.Teaches = new List<Teaches>{
                new Teaches{
                    Course = course,
                    InstructorId=1, SemesterId=1
                }
            };
            course.CoursePIs = new List<CoursePI>{
                new CoursePI{
                    Course = course,
                    PerformanceIndicatorId=1
                },
                new CoursePI{
                    Course = course,
                    PerformanceIndicatorId=2
                }
            };
            dataContext.Courses.AddAsync(course);

            for (int i = 1; i < 10; ++i)
            {
                dataContext.Takes.AddAsync(new Takes { SemesterId = 1, StudentId = i, CourseId = 1 });
            }

            await dataContext.SaveChangesAsync();
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
