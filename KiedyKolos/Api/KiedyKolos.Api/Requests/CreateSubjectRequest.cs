﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiedyKolos.Api.Requests
{
    public class CreateSubjectRequest
    {
        public int YearCourseId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
