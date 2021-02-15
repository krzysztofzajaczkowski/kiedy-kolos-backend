using System;
using System.Collections.Generic;

namespace KiedyKolos.Core.Models
{
  public class Event
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }


    public int SubjectId { get; set; }
    public Subject Subject { get; set; }
    public int EventTypeId { get; set; }
    public EventType EventType { get; set; }
    public int YearCourseId { get; set; }
    public YearCourse YearCourse { get; set; }

    public ICollection<GroupEvent> GroupEvents { get; set; }
  }
}