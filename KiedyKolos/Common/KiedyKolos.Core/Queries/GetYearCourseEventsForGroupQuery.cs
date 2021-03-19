using System;
using System.Collections.Generic;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolor.Core.Queries
{
    public class GetYearCourseEventsForGroupQuery : IRequest<BaseResult<List<Event>>>
    {
        public int YearCourseId { get; set; }
        public int GroupId { get; set; }
        public DateTime? Date { get; set; }
    }
}