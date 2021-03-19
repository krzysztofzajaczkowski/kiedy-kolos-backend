using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class CreateEventTypeCommandHandler : IRequestHandler<CreateEventTypeCommand, BaseResult<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEventTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult<int>> Handle(CreateEventTypeCommand request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                return BaseResult<int>.Fail(ErrorType.NotFound, new List<string>
                {
                    "Year course not found!"
                });
            }

            var eventType = new EventType()
            {
                Name = request.Name
            };

            await _unitOfWork.EventTypeRepository.AddAsync(eventType);
            await _unitOfWork.CommitAsync();

            return BaseResult<int>.Success(ResultType.Created, eventType.Id);
        }
    }
}