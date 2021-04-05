using System;
using System.Linq;
using System.Text;
using KiedyKolos.Core.Middleware.Interfaces;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class DeleteYearCourseSubjectCommand : IRequest<BaseResult>, IAuthorizable
    {
        public int YearCourseId { get; set; }
        public string Password { get; set; }
        public int SubjectId { get; set; }
    }
}
