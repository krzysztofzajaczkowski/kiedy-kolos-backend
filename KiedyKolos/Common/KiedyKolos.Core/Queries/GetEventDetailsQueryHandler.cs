using System.Collections.Generic;
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
            var eventToFetch = await _unitOfWork.EventRepository.GetAsync(request.EventId);

            if (eventToFetch == null)
            {
                return BaseResult<Event>.Fail(ErrorType.NotFound,
                    new List<string>
                    {
                        "Resource not found!"
                    });
            }

            return BaseResult<Event>.Success(ResultType.Ok, eventToFetch);
        }
    }
}