using System;
using System.Linq;
using System.Net;
using System.Text;
using KiedyKolos.Core.Middleware.Interfaces;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class DeleteYearCourseGroupCommand : IRequest<BaseResult>, IAuthorizable
    {
        public int YearCourseId { get; set; }
        public string Password { get; set; }
        public int GroupId { get; set; }
    }
}
