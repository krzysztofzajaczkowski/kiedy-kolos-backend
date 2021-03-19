using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiedyKolos.Api.Requests
{
    public class AuthRequest
    {
        public int YearCourseId { get; set; }
        public string Password { get; set; }
    }
}
