using KiedyKolos.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiedyKolos.Infrastructure.Data.Configuration
{
  public class EventTypeConfiguration : IEntityTypeConfiguration<EventType>
  {
    public void Configure(EntityTypeBuilder<EventType> builder)
    {
      builder.HasKey(e => e.Id);

      builder.Property(e => e.Name)
        .HasMaxLength(20);

      builder.HasMany<Event>(e => e.Events)
        .WithOne(e => e.EventType)
        .HasForeignKey(e => e.EventTypeId);
    }
  }
}