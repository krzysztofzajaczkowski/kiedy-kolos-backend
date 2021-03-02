using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetYearCourseSubjectByIdQueryHandler : IRequestHandler<GetYearCourseSubjectByIdQuery, BaseResult<Subject>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetYearCourseSubjectByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult<Subject>> Handle(GetYearCourseSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                return BaseResult<Subject>.Fail(ErrorType.NotFound, new List<string>
                {
                    "Year course not found!"
                });
            }

            var subject = await _unitOfWork.SubjectRepository.GetYearCourseSubject(request.YearCourseId, request.SubjectId);

            if (subject == null)
            {
                return BaseResult<Subject>.Fail(ErrorType.NotFound, new List<string>
                {
                    "Subject not found!"
                });
            }

            return BaseResult<Subject>.Success(ResultType.Ok, subject);
        }
    }
}