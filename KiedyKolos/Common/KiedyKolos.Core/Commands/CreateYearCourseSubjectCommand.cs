using System;
using System.Linq;
using System.Net;
using System.Text;
using KiedyKolos.Core.Middleware.Interfaces;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class CreateYearCourseSubjectCommand : IRequest<BaseResult<int>>, IAuthorizable
    {
        public int YearCourseId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
