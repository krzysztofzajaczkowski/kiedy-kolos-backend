using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class CreateYearCourseSubjectCommandHandler : IRequestHandler<CreateYearCourseSubjectCommand, BaseResult<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateYearCourseSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult<int>> Handle(CreateYearCourseSubjectCommand request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                return BaseResult<int>.Fail(ErrorType.NotFound, new List<string>
                {
                    "Resource not found!"
                });
            }

            var subject = new Subject
            {
                Name = request.Name,
                ShortName = request.ShortName,
                YearCourseId = request.YearCourseId
            };

            await _unitOfWork.SubjectRepository.AddAsync(subject);
            await _unitOfWork.CommitAsync();

            return BaseResult<int>.Success(ResultType.Created, subject.Id);
        }
    }
}