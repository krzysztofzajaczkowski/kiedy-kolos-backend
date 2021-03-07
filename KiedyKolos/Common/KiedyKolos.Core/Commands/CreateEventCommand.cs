using System;
using KiedyKolos.Core.Middleware.Interfaces;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class CreateEventCommand : IAuthorizable, IRequest<BaseResult<int>>
    {
        public int YearCourseId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int SubjectId { get; set; }
        public int EventTypeId { get; set; }
    }
}