﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetYearCourseByIdQuery : IRequest<BaseResult<YearCourse>>
    {
        public int YearCourseId { get; set; }
    }
}