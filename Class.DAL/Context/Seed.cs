using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace School.DAL.Context
{
    public static class Seed
    {
        public static async Task Initialize(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<SchoolDbContext>();

            if (!context.Roles.Any())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

                await roleManager.CreateAsync(new IdentityRole<int>(UserRole.TEACHER));

                await roleManager.CreateAsync(new IdentityRole<int>(UserRole.STUDENT));
            }
        }
    }
}
