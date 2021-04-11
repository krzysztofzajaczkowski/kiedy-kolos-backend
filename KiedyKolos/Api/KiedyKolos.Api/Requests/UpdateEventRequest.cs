using System;
using System.Collections.Generic;

namespace KiedyKolos.Api.Requests
{
    public class UpdateEventRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int YearCourseId { get; set; }
        public int EventTypeId { get; set; }
        public int SubjectId { get; set; }
        public List<int> GroupIds { get; set; }
    }
}