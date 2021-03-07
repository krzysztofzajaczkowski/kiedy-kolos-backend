using System;

namespace KiedyKolos.Api.Requests
{
    public class UpdateEventRequest
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int YearCourseId { get; set; }
        public int EventTypeId { get; set; }
        public int SubjectId { get; set; }
    }
}