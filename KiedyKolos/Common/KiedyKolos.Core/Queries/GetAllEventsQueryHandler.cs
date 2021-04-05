using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, BaseResult<List<Event>>>
    {
        private IUnitOfWork _unitOfWork;

        public GetAllEventsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult<List<Event>>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _unitOfWork.EventRepository.GetAllAsync();
            return BaseResult<List<Event>>.Success(ResultType.Ok, events);
        }
    }
}