using Microsoft.Extensions.DependencyInjection;
using School.BLL.Interfaces;
using School.BLL.Profiles;
using School.BLL.Services;

namespace School.BLL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBLLService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ClassProfile), typeof(ClassSubjectProfile), typeof(GradeProfile), typeof(HomeworkProfile), 
                typeof(HomeworkSubmissionProfile), typeof(ScheduleProfile), typeof(SubjectProfile), typeof(SubjectProfile));

            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IClassSubjectService, ClassSubjectService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<IHomeworkService, HomeworkService>();
            services.AddScoped<IHomeworkSubmissionService, HomeworkSubmissionService>();
            services.AddScoped<IScheduleService, ScheduleSrvice>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
