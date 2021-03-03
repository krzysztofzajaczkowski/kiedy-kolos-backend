using System;
using System.Linq;
using System.Text;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetYearCourseSubjectByIdQuery : IRequest<BaseResult<Subject>>
    {
        public int YearCourseId { get; set; }
        public int SubjectId { get; set; }
    }
}
