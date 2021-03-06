using KiedyKolos.Core.Middleware.Interfaces;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolor.Core.Commands
{
    public class DeleteEventCommand :  IRequest<BaseResult>, IAuthorizable
    {
        public int YearCourseId { get; set; }
        public int EventId { get; set; }
        public string Password { get; set; }
    }
}