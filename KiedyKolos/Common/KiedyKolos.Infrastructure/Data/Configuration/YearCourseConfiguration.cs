using KiedyKolos.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiedyKolos.Infrastructure.Data.Configuration
{
  public class YearCourseConfiguration : IEntityTypeConfiguration<YearCourse>
  {
    public void Configure(EntityTypeBuilder<YearCourse> builder)
    {
      builder.HasKey(e => e.Id);
      builder.Property(e => e.Course)
        .HasMaxLength(40);
      builder.Property(e => e.Faculty)
        .HasMaxLength(60);
      builder.Property(e => e.University)
        .HasMaxLength(30);

      builder.HasMany<Subject>(e => e.Subjects)
        .WithOne(e => e.YearCourse)
        .HasForeignKey(e => e.YearCourseId);

      builder.HasMany<Group>(e => e.Groups)
        .WithOne(e => e.YearCourse)
        .HasForeignKey(e => e.YearCourseId);

      builder.HasMany<Event>(e => e.Events)
        .WithOne(e => e.YearCourse)
        .HasForeignKey(e => e.YearCourseId);
    }
  }
}