using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    class GetAllYearCoursesQueryHandler: IRequestHandler<GetAllYearCoursesQuery, BaseResult<List<YearCourse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllYearCoursesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult<List<YearCourse>>> Handle(GetAllYearCoursesQuery request, CancellationToken cancellationToken)
        {
            var yearCourses =  await _unitOfWork.YearCourseRepository.GetAllAsync();
            return BaseResult<List<YearCourse>>.Success(ResultType.Ok, yearCourses);
        }
    }
}
