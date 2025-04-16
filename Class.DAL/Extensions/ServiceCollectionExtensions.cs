using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.DAL.Context;
using School.DAL.Entities;
using School.DAL.Interfaces;
using School.DAL.Repository;

namespace School.DAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDALService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SchoolDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<SchoolDbContext>()
                .AddDefaultTokenProviders();

            services.AddMemoryCache();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IClassSubjectRepository, ClassSubjectRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<IHomeworkRepository, HomeworkRepository>();
            services.AddScoped<IHomeworkSubmissionsRepository, HomeworkSubmissionsRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}