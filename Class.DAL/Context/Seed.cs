using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using School.DAL.Entities;
using System.Net.WebSockets;

namespace School.DAL.Context
{
    public static class Seed
    {
        public static async Task Initialize(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<SchoolDbContext>();

            if (!await context.Roles.AnyAsync())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

                await roleManager.CreateAsync(new IdentityRole<int>(UserRole.TEACHER));

                await roleManager.CreateAsync(new IdentityRole<int>(UserRole.STUDENT));

                await roleManager.CreateAsync(new IdentityRole<int>(UserRole.ADMIN));
            }

            if (!await context.Users.AnyAsync())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                var adminRole = context.Roles.FirstOrDefault(r => r.Name == UserRole.ADMIN);

                if (adminRole != null)
                {
                    var adminUser = new User
                    {
                        UserName = "admin",
                        Email = "admin@gmail.com",
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var createResult = await userManager.CreateAsync(adminUser, "Admin123!");

                    if (createResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, UserRole.ADMIN);
                    }
                }
                //await context.Users.AddAsync(adminUser);
                //await context.SaveChangesAsync();
            }
        }
    }
}
