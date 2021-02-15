using System.Collections.Generic;

namespace KiedyKolos.Core.Models
{
  public class Subject
  {
    public int Id { get; set; }
    public int YearCourseId { get; set; }
    public YearCourse YearCourse { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }

    public ICollection<Event> Events { get; set; }
  }
}