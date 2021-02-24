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
    public class IncrementSemesterCommandHandler: IRequestHandler<IncrementSemesterCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public IncrementSemesterCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult> Handle(IncrementSemesterCommand request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            yearCourse.CurrentSemester += request.IncrementBy;
            await _unitOfWork.YearCourseRepository.UpdateAsync(yearCourse);
            await _unitOfWork.CommitAsync();

            return BaseResult.Success(ResultType.Ok);

        }
    }
}
