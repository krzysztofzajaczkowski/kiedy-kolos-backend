using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiedyKolos.Api.Responses
{
    public class GetBlockYearCourseResponse
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public string University { get; set; }
        public int CurrentSemester { get; set; }
    }
}
