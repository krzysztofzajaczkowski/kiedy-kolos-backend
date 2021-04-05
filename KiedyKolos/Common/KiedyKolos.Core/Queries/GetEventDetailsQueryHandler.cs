using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetEventDetailsQueryHandler : IRequestHandler<GetEventDetailsQuery, BaseResult<Event>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEventDetailsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult<Event>> Handle(GetEventDetailsQuery request, CancellationToken cancellationToken)
        {
            //Check if year course exists & if request.eventId belongs to this yearCourse
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            var eventToFetch = await _unitOfWork.EventRepository.GetAsync(request.EventId);
            if (eventToFetch == null || yearCourse == null)
            {
                return BaseResult<Event>.Fail(ErrorType.NotFound,
                    new List<string>
                    {
                        "Resource not found!"
                    });
            }

            if (!yearCourse.Events.Any(x => x.Id == request.EventId))
            {
                return BaseResult<Event>.Fail(ErrorType.NotFound,
                new List<string>
                {
                    "Requested event doesnt belong to given YearCourse!"
                });
            }

            return BaseResult<Event>.Success(ResultType.Ok, eventToFetch);
        }
    }
}