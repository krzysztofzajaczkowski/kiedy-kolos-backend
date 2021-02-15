using KiedyKolos.Core.Models;
using KiedyKolos.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace KiedyKolos.Infrastructure.Data.Context
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new EventConfiguration());
      modelBuilder.ApplyConfiguration(new GroupConfiguration());
      modelBuilder.ApplyConfiguration(new SubjectConfiguration());
      modelBuilder.ApplyConfiguration(new YearCourseConfiguration());
      modelBuilder.ApplyConfiguration(new KeyConfiguration());
      modelBuilder.ApplyConfiguration(new EventTypeConfiguration());
      modelBuilder.ApplyConfiguration(new GroupEventConfiguration());
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<YearCourse> YearCourses { get; set; }
    public DbSet<Key> Keys { get; set; }
    public DbSet<EventType> EventTypes { get; set; }
    public DbSet<GroupEvent> GroupEvents { get; set; }
  }
}