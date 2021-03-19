using System.Collections.Generic;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class GetAllEventsQuery : IRequest<BaseResult<List<Event>>>
    {
    }
}