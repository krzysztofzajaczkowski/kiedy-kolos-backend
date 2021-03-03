using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class DeleteYearCourseSubjectCommandHandler : IRequestHandler<DeleteYearCourseSubjectCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteYearCourseSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult> Handle(DeleteYearCourseSubjectCommand request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                return BaseResult.Fail(ErrorType.NotFound, new List<string>
                {
                    "Year course not found!"
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

            await _unitOfWork.SubjectRepository.DeleteAsync(request.SubjectId);
            await _unitOfWork.CommitAsync();

            return BaseResult.Success(ResultType.Deleted);
        }
    }
}