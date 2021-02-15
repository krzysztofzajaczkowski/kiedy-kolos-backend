using System.Collections.Generic;

namespace KiedyKolos.Core.Models
{
  public class YearCourse
  {
    public int Id { get; set; }
    public string Course { get; set; }
    public int CourseStartYear { get; set; }
    public string Faculty { get; set; }
    public string University { get; set; }
    public int CurrentSemester { get; set; }
    public string Password { get; set; }

    public ICollection<Event> Events { get; set; }
    public ICollection<Group> Groups { get; set; }
    public ICollection<Subject> Subjects { get; set; }

  }
}