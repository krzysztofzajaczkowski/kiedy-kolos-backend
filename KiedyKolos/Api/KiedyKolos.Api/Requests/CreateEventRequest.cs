using System;

namespace KiedyKolos.Api.Requests
{
    public class CreateEventRequest
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int YearCourseId { get; set; }
    }
}