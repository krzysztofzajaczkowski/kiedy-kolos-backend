using System;
using System.Linq;
using System.Text;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetYearCourseGroupByIdQuery : IRequest<BaseResult<Group>>
    {
        public int YearCourseId { get; set; }
        public int GroupId { get; set; }
    }
}
