using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetAllYearCourseSubjectsQueryHandler : IRequestHandler<GetAllYearCourseSubjectsQuery, BaseResult<List<Subject>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllYearCourseSubjectsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult<List<Subject>>> Handle(GetAllYearCourseSubjectsQuery request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                return BaseResult<List<Subject>>.Fail(ErrorType.NotFound, new List<string>
                {
                    "Year course not found!"
                });
            }

            var subjects = await _unitOfWork.SubjectRepository.GetAllForYearCourseAsync(request.YearCourseId);

            return BaseResult<List<Subject>>.Success(ResultType.Ok, subjects);
        }
    }
}