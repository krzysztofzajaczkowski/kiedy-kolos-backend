using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiedyKolos.Core.Middleware.Interfaces
{
    public interface IAuthorizable
    {
        int YearCourseId { get; set; }
        string Password { get; set; }
    }
}
