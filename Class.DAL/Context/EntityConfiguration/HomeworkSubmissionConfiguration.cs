using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.DAL.Entities;

namespace School.DAL.Context.EntityConfiguration
{
    public class HomeworkSubmissionConfiguration : IEntityTypeConfiguration<HomeworkSubmission>
    {
        public void Configure(EntityTypeBuilder<HomeworkSubmission> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Student)
                .WithMany(s => s.HomeworkSubmissions)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Homework)
                .WithMany()
                .HasForeignKey(x => x.HomeworkId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.FilePath)
                .IsRequired();

            builder.Property(x => x.SubmittedAt)
                .IsRequired();
        }
    }
}
