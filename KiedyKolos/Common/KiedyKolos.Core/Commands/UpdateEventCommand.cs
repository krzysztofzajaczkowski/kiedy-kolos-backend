using System;
using KiedyKolos.Core.Middleware.Interfaces;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class UpdateEventCommand : IRequest<BaseResult>, IAuthorizable
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int YearCourseId { get; set; }
        public string Password { get; set; }
    }
}