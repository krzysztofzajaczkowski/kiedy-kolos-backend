using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiedyKolos.Core.Middleware.Interfaces;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class UpdateYearCourseCommand :  IRequest<BaseResult>, IAuthorizable
    {
        public int YearCourseId { get; set; }
        public string Course { get; set; }
        public int CourseStartYear { get; set; }
        public string Faculty { get; set; }
        public string University { get; set; }
        public int CurrentSemester { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
