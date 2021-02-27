using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class DeleteYearCourseCommandHandler : IRequestHandler<DeleteYearCourseCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteYearCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult> Handle(DeleteYearCourseCommand request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                return BaseResult.Fail(ErrorType.NotFound,
                    new List<string>
                    {
                        "Resource not found!"
                    });
            }

            await _unitOfWork.YearCourseRepository.DeleteAsync(request.YearCourseId);
            await _unitOfWork.CommitAsync();

            return BaseResult.Success(ResultType.Deleted);
        }
    }
}
