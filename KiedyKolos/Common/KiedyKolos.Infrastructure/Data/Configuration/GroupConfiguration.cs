using KiedyKolos.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiedyKolos.Infrastructure.Data.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany<GroupEvent>(e => e.GroupEvents)
            .WithOne(e => e.Group)
            .HasForeignKey(e => e.GroupId);

            builder.HasOne<YearCourse>(e => e.YearCourse)
            .WithMany(e => e.Groups)
            .HasForeignKey(e => e.YearCourseId);
            
        }
    }
}