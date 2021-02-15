using KiedyKolos.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiedyKolos.Infrastructure.Data.Configuration
{
  public class EventConfiguration : IEntityTypeConfiguration<Event>
  {
    public void Configure(EntityTypeBuilder<Event> builder)
    {
      builder.HasKey(e => e.Id);

      builder.HasMany<GroupEvent>(e => e.GroupEvents)
        .WithOne(g => g.Event)
        .HasForeignKey(e => e.EventId);

      builder.HasOne<YearCourse>(e => e.YearCourse)
        .WithMany(e => e.Events)
        .HasForeignKey(g => g.YearCourseId);

      builder.HasOne<Subject>(e => e.Subject)
        .WithMany(e => e.Events)
        .HasForeignKey(e => e.SubjectId);

      builder.HasOne<EventType>(e => e.EventType)
        .WithMany(e => e.Events)
        .HasForeignKey(e => e.EventTypeId);
    }
  }
}