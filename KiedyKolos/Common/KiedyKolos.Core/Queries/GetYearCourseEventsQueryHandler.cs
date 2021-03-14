using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolor.Core.Queries
{
    public class GetYearCourseEventsQueryHandler : IRequestHandler<GetYearCourseEventsQuery, BaseResult<List<Event>>>
    {   
        private IUnitOfWork _unitOfWork;

        public GetYearCourseEventsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult<List<Event>>> Handle(GetYearCourseEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _unitOfWork.EventRepository.GetYearCourseEventAsync(request.YearCourseId, request.Date, request.GroupIds);
            return BaseResult<List<Event>>.Success(ResultType.Ok, events);
        }
    }
}