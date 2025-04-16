using School.DAL.Extensions;
using School.BLL.Extensions;
using School.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace School.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDALService(builder.Configuration);
            builder.Services.AddBLLService();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            using (var scoped = app.Services.CreateScope())
            {
                var dbContext = scoped.ServiceProvider.GetRequiredService<SchoolDbContext>();

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    var pendingMigration = dbContext.Database.GetPendingMigrations();

                    foreach (var migration in pendingMigration)
                    {
                        Console.WriteLine($"Pending Migration: {migration}");
                    }

                    await dbContext.Database.MigrateAsync();
                }

                await Seed.Initialize(scoped);
            }

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
