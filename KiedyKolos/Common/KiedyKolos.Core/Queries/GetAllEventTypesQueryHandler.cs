using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetAllEventTypesQueryHandler : IRequestHandler<GetAllEventTypesQuery, BaseResult<List<EventType>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllEventTypesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult<List<EventType>>> Handle(GetAllEventTypesQuery request, CancellationToken cancellationToken)
        {
            var eventTypes = await _unitOfWork.EventTypeRepository.GetAllAsync();

            return BaseResult<List<EventType>>.Success(ResultType.Ok, eventTypes);
        }
    }
}