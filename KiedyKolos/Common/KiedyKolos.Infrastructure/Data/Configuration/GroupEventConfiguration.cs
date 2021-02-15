using KiedyKolos.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiedyKolos.Infrastructure.Data.Configuration
{
    public class GroupEventConfiguration : IEntityTypeConfiguration<GroupEvent>
    {
        public void Configure(EntityTypeBuilder<GroupEvent> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne<Event>(e => e.Event)
            .WithMany(e => e.GroupEvents)
            .HasForeignKey(e => e.EventId);

            builder.HasOne<Group>(e => e.Group)
            .WithMany(e => e.GroupEvents)
            .HasForeignKey(e => e.GroupId);
        }
    }
}