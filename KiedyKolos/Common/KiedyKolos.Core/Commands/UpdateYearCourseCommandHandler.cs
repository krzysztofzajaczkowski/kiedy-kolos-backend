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

namespace KiedyKolos.Core.Commands
{
    public class UpdateYearCourseCommandHandler : IRequestHandler<UpdateYearCourseCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateYearCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult> Handle(UpdateYearCourseCommand request, CancellationToken cancellationToken)
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
            await _unitOfWork.YearCourseRepository.UpdateAsync(new YearCourse
            {
                Password = request.NewPassword,
                CurrentSemester = request.CurrentSemester,
                Course = request.Course,
                CourseStartYear = request.CourseStartYear,
                Faculty = request.Faculty,
                University = request.University,
                Id = request.YearCourseId
            });

            await _unitOfWork.CommitAsync();

            return BaseResult.Success(ResultType.Ok);
        }
    }
}
