using System.Collections.Generic;

namespace KiedyKolos.Core.Models
{
  public class EventType
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Event> Events { get; set; }
  }
}