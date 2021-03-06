using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetAllYearCourseGroupsQueryHandler : IRequestHandler<GetAllYearCourseGroupsQuery, BaseResult<List<Group>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllYearCourseGroupsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult<List<Group>>> Handle(GetAllYearCourseGroupsQuery request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                return BaseResult<List<Group>>.Fail(ErrorType.NotFound, new List<string>
                {
                    "Year course not found!"
                });
            }

            var groups = await _unitOfWork.GroupRepository.GetAllForYearCourseAsync(request.YearCourseId);

            return BaseResult<List<Group>>.Success(ResultType.Ok, groups);
        }
    }
}