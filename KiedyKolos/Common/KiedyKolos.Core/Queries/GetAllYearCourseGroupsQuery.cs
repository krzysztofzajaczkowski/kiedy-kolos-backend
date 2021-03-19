using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetAllYearCourseGroupsQuery : IRequest<BaseResult<List<Group>>>
    {
        public int YearCourseId { get; set; }
    }
}
