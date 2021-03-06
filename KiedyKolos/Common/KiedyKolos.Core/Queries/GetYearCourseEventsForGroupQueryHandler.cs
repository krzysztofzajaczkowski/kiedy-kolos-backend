using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolor.Core.Queries;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetYearCourseEventsForGroupQueryHandler : IRequestHandler<GetYearCourseEventsForGroupQuery, BaseResult<List<Event>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetYearCourseEventsForGroupQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult<List<Event>>> Handle(GetYearCourseEventsForGroupQuery request, CancellationToken cancellationToken)
        {
            var events = await _unitOfWork.EventRepository.GetYearCourseEventsForGroupAsync(request.YearCourseId,request.GroupId, request.Date);
            return BaseResult<List<Event>>.Success(ResultType.Ok, events);
        }
    }
}