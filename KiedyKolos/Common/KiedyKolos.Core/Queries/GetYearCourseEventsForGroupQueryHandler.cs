using System.Collections.Generic;
using System.Linq;
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
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                return BaseResult<List<Event>>.Fail(ErrorType.NotFound,
                    new List<string>
                    {
                        "Year course does not exist!"
                    });
            }
            if(!yearCourse.Groups.Any(x => x.Id == request.GroupId)){
                return BaseResult<List<Event>>.Fail(ErrorType.NotFound,
                    new List<string>
                    {
                        "Given group does not exist or does not belong to given year course!"
                    });
            }
            var events = await _unitOfWork.EventRepository.GetYearCourseEventsForGroupAsync(request.YearCourseId,request.GroupId, request.Date);

            return BaseResult<List<Event>>.Success(ResultType.Ok, events);
        }
    }
}