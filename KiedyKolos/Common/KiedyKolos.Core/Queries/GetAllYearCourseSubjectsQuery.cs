using System.Collections.Generic;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetAllYearCourseSubjectsQuery : IRequest<BaseResult<List<Subject>>>
    {
        public int YearCourseId { get; set; }
    }
}
