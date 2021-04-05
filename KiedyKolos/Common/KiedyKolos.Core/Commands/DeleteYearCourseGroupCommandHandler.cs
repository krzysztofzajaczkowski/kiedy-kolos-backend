using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class DeleteYearCourseGroupCommandHandler : IRequestHandler<DeleteYearCourseGroupCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteYearCourseGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult> Handle(DeleteYearCourseGroupCommand request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                return BaseResult.Fail(ErrorType.NotFound, new List<string>
                {
                    "Year course not found!"
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

            await _unitOfWork.GroupRepository.DeleteAsync(request.GroupId);
            await _unitOfWork.CommitAsync();

            return BaseResult.Success(ResultType.Deleted);
        }
    }
}