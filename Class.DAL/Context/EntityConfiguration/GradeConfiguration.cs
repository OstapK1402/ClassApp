using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.DAL.Entities;

namespace School.DAL.Context.EntityConfiguration
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Teacher)
                .WithMany(t => t.GradesGiven)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.HomeworkSubmission)
                .WithMany()
                .HasForeignKey(x => x.HomeworkSubmissionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.GradeValue)
                .IsRequired();

            builder.Property(x => x.Comment)
                .HasMaxLength(500);

            builder.Property(x => x.Date)
                .IsRequired();
        }
    }
}
