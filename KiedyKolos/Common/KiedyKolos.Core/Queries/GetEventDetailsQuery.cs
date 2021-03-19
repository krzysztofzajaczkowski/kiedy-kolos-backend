using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetEventDetailsQuery : IRequest<BaseResult<Event>>
    {
        public int YearCourseId { get; set; }
        public int EventId { get; set; }
    }
}