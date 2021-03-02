using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class UpdateYearCourseSubjectCommandHandler : IRequestHandler<UpdateYearCourseSubjectCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateYearCourseSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult> Handle(UpdateYearCourseSubjectCommand request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                return BaseResult<int>.Fail(ErrorType.NotFound, new List<string>
                {
                    "Resource not found!"
                });
            }

            var subject = await _unitOfWork.SubjectRepository.GetYearCourseSubject(request.YearCourseId, request.SubjectId);

            if (subject == null)
            {
                return BaseResult.Fail(ErrorType.NotFound, new List<string>
                {
                    "Subject not found!"
                });
            }

            await _unitOfWork.SubjectRepository.UpdateAsync(new Subject
            {
                Id = request.SubjectId,
                YearCourseId = request.YearCourseId,
                Name = request.Name,
                ShortName = request.ShortName
            });
            await _unitOfWork.CommitAsync();

            return BaseResult.Success(ResultType.Ok);
        }
    }
}