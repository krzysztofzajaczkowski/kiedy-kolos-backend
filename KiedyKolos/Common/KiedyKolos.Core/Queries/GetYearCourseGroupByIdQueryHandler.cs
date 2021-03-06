using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetYearCourseGroupByIdQueryHandler : IRequestHandler<GetYearCourseGroupByIdQuery, BaseResult<Group>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetYearCourseGroupByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult<Group>> Handle(GetYearCourseGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                return BaseResult<Group>.Fail(ErrorType.NotFound, new List<string>
                {
                    "Year course not found!"
                });
            }

            var group = await _unitOfWork.GroupRepository.GetYearCourseGroup(request.YearCourseId, request.GroupId);

            if (group == null)
            {
                return BaseResult<Group>.Fail(ErrorType.NotFound, new List<string>
                {
                    "Group not found!"
                });
            }

            return BaseResult<Group>.Success(ResultType.Ok, group);
        }
    }
}