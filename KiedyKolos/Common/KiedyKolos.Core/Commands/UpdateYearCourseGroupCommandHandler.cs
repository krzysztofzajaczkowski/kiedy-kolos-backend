using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class UpdateYearCourseGroupCommandHandler : IRequestHandler<UpdateYearCourseGroupCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateYearCourseGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult> Handle(UpdateYearCourseGroupCommand request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                return BaseResult<int>.Fail(ErrorType.NotFound, new List<string>
                {
                    "Resource not found!"
                });
            }

            var group = await _unitOfWork.GroupRepository.GetYearCourseGroup(request.YearCourseId, request.GroupId);

            if (group == null)
            {
                return BaseResult.Fail(ErrorType.NotFound, new List<string>
                {
                    "Group not found!"
                });
            }

            await _unitOfWork.GroupRepository.UpdateAsync(new Group
            {
                Id = request.GroupId,
                YearCourseId = request.YearCourseId,
                GroupName = request.GroupName,
                GroupNumber = request.GroupNumber
            });
            await _unitOfWork.CommitAsync();

            return BaseResult.Success(ResultType.Ok);
        }
    }
}