using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, BaseResult<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateEventCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult<int>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                return BaseResult<int>.Fail(ErrorType.NotFound, new List<string>
                {
                    "Resource not found!"
                });
            }

            var eventToAdd = new Event
            {
                Name = request.Name,
                Description = request.Description,
                YearCourseId = request.YearCourseId,
                Date = request.Date,
                EventTypeId = request.EventTypeId,
                SubjectId = request.SubjectId
            };

            await _unitOfWork.EventRepository.AddAsync(eventToAdd);
            await _unitOfWork.CommitAsync();

            return BaseResult<int>.Success(ResultType.Created, eventToAdd.Id);
        }
    }
}