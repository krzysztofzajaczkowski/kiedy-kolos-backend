using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEventCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
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
            await _unitOfWork.EventRepository.UpdateAsync(new Event
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                YearCourseId = request.YearCourseId,
                Date = request.Date,
                SubjectId = request.SubjectId,
                EventTypeId = request.EventTypeId
            });

            await _unitOfWork.CommitAsync();

            return BaseResult.Success(ResultType.Ok);
        }
    }
}
