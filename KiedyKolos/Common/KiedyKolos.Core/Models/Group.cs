using System.Collections.Generic;

namespace KiedyKolos.Core.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int YearCourseId { get; set; }
        public YearCourse YearCourse { get; set; }
        public int GroupNumber { get; set; }
        public string GroupName { get; set; }

        public ICollection<GroupEvent> GroupEvents { get; set; }
    }
}