using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolor.Core.Commands
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEventCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventToDelete = await _unitOfWork.EventRepository.GetAsync(request.EventId);
            if (eventToDelete == null)
            {
                return BaseResult.Fail(ErrorType.NotFound,
                    new List<string>
                    {
                        "Resource not found!"
                    });
            }

            await _unitOfWork.EventRepository.DeleteAsync(request.EventId);
            await _unitOfWork.CommitAsync();

            return BaseResult.Success(ResultType.Deleted);
        }
    }
}