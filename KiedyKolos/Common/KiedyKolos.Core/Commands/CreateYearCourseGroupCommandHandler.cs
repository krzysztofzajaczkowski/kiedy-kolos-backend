using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class CreateYearCourseGroupCommandHandler : IRequestHandler<CreateYearCourseGroupCommand, BaseResult<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateYearCourseGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult<int>> Handle(CreateYearCourseGroupCommand request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                return BaseResult<int>.Fail(ErrorType.NotFound, new List<string>
                {
                    "Resource not found!"
                });
            }

            var group = new Group
            {
                GroupName = request.GroupName,
                GroupNumber = request.GroupNumber,
                YearCourseId = request.YearCourseId
            };

            await _unitOfWork.GroupRepository.AddAsync(group);
            await _unitOfWork.CommitAsync();

            return BaseResult<int>.Success(ResultType.Created, group.Id);
        }
    }
}