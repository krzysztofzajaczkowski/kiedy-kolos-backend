namespace KiedyKolos.Core.Models
{
  public class GroupEvent
  {
    public int Id { get; set; }
    public int GroupId { get; set; }
    public Group Group {get;set;}
    public int EventId { get; set; }
    public Event Event {get;set;}
  }
}