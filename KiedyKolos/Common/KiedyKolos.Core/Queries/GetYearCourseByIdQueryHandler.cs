using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetYearCourseByIdQueryHandler : IRequestHandler<GetYearCourseByIdQuery, BaseResult<YearCourse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetYearCourseByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult<YearCourse>> Handle(GetYearCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);

            if (yearCourse == null)
            {
                return BaseResult<YearCourse>.Fail(ErrorType.NotFound,
                    new List<string>
                    {
                        "Resource not found!"
                    });
            }

            return BaseResult<YearCourse>.Success(ResultType.Ok, yearCourse);
        }
    }
}
