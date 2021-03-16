using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KiedyKolos.Core.Middleware.Interfaces;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class YearCourseAuthQuery : IRequest<BaseResult>, IAuthorizable
    {
        public int YearCourseId { get; set; }
        public string Password { get; set; }
    }
}
