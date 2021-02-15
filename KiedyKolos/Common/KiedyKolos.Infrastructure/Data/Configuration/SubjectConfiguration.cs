using KiedyKolos.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiedyKolos.Infrastructure.Data.Configuration
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
            .HasMaxLength(40);
            builder.Property(e => e.ShortName)
            .HasMaxLength(10);

            builder.HasMany<Event>(e => e.Events)
            .WithOne(e => e.Subject)
            .HasForeignKey(e => e.SubjectId);

            builder.HasOne<YearCourse>(e => e.YearCourse)
            .WithMany(e => e.Subjects)
            .HasForeignKey(e => e.YearCourseId);
        }
    }
}