using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KiedyKolor.Core.Commands;
using KiedyKolor.Core.Queries;
using KiedyKolos.Api.Configuration;
using KiedyKolos.Api.Requests;
using KiedyKolos.Api.Responses;
using KiedyKolos.Core.Commands;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KiedyKolos.Api.Controllers
{
    [ApiController]
    [Route("yearCourses/{yearCourseId}/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly AuthOptions _options;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public EventsController(IOptions<AuthOptions> options,
            IMediator mediator,
            IMapper mapper)
        {
            _options = options.Value;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/[controller]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new GetAllEventsQuery());

            if (!result.Succeeded)
            {
                return StatusCode((int?)result.ErrorType ?? 400, new ApiResponse
                {
                    Messages = result.ErrorMessages
                });
            }

            var dto = _mapper.Map<List<GetEventResponse>>(result.Output);

            return Ok(new ApiResponse<List<GetEventResponse>>
            {
                Result = dto
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetYearCourseEventsAsync(int yearCourseId, [FromQuery] DateTime? date, [FromQuery] List<int> groupIds)
        {
            var result = await _mediator.Send(new GetYearCourseEventsQuery
            {
                YearCourseId = yearCourseId,
                Date = date,
                GroupIds = groupIds
            });

            if (!result.Succeeded)
            {
                return StatusCode((int?)result.ErrorType ?? 400, new ApiResponse
                {
                    Messages = result.ErrorMessages
                });
            }

            var dto = _mapper.Map<List<GetEventResponse>>(result.Output);

            return Ok(new ApiResponse<List<GetEventResponse>>
            {
                Result = dto
            });
        }

        [HttpGet("/yearCourses/{yearCourseId}/groups/{groupId}/[controller]")]
        public async Task<IActionResult> GetYearCourseEventsForGroupAsync(int yearCourseId, int groupId, [FromQuery] DateTime? date)
        {
            var result = await _mediator.Send(new GetYearCourseEventsForGroupQuery
            {
                YearCourseId = yearCourseId,
                GroupId = groupId,
                Date = date
            });

            if (!result.Succeeded)
            {
                return StatusCode((int?)result.ErrorType ?? 400, new ApiResponse
                {
                    Messages = result.ErrorMessages
                });
            }

            var dto = _mapper.Map<List<GetEventResponse>>(result.Output);

            return Ok(new ApiResponse<List<GetEventResponse>>
            {
                Result = dto
            });
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventDetailsAsync(int yearCourseId, int eventId)
        {
            var result = await _mediator.Send(new GetEventDetailsQuery
            {
                YearCourseId = yearCourseId,
                EventId = eventId
            });

            if (!result.Succeeded)
            {
                return StatusCode((int?)result.ErrorType ?? 400, new ApiResponse
                {
                    Messages = result.ErrorMessages
                });
            }

            var dto = _mapper.Map<GetEventResponse>(result.Output);

            return Ok(new ApiResponse<GetEventResponse>
            {
                Result = dto
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddEventToYearCourseAsync(int yearCourseId, CreateEventRequest request)
        {
            var apiKey = Request.Headers.FirstOrDefault(h => string.Equals(h.Key, _options.ApiKeyHeaderName, StringComparison.CurrentCultureIgnoreCase))
                .Value;

            if (yearCourseId != request.YearCourseId)
            {
                return BadRequest(new ApiResponse
                {
                    Messages = new List<string>
                    {
                        "Year course ID doesn't match!"
                    }
                });
            }

            var result = await _mediator.Send(new CreateEventCommand
            {
                YearCourseId = request.YearCourseId,
                Password = apiKey,
                Name = request.Name,
                Description = request.Description,
                Date = request.Date,
                SubjectId = request.SubjectId,
                EventTypeId = request.EventTypeId
            });

            if (!result.Succeeded)
            {
                return StatusCode((int?)result.ErrorType ?? 400, new ApiResponse<int>
                {
                    Messages = result.ErrorMessages
                });
            }

            return Ok(new ApiResponse<int>
            {
                Result = result.Output
            });
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateEventDetailsAsync(int yearCourseId, int eventId, UpdateEventRequest request)
        {
            var apiKey = Request.Headers.FirstOrDefault(h => string.Equals(h.Key, _options.ApiKeyHeaderName, StringComparison.CurrentCultureIgnoreCase))
                            .Value;

            if (yearCourseId != request.YearCourseId)
            {
                return BadRequest(new ApiResponse
                {
                    Messages = new List<string>
                    {
                        "Year course ID doesn't match!"
                    }
                });
            }

            var result = await _mediator.Send(new UpdateEventCommand
            {
                YearCourseId = request.YearCourseId,
                Password = apiKey,
                Name = request.Name,
                Description = request.Description,
                Date = request.Date,
                SubjectId = request.SubjectId,
                EventTypeId = request.EventTypeId
            });

            if (!result.Succeeded)
            {
                return StatusCode((int?)result.ErrorType ?? 400, new ApiResponse<int>
                {
                    Messages = result.ErrorMessages
                });
            }

            return Ok(new ApiResponse());
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEventAsync(int yearCourseId, int eventId)
        {
            var apiKey = Request.Headers.FirstOrDefault(h => string.Equals(h.Key, _options.ApiKeyHeaderName, StringComparison.CurrentCultureIgnoreCase))
                .Value;

            var result = await _mediator.Send(new DeleteEventCommand
            {
                YearCourseId = yearCourseId,
                EventId = eventId,
                Password = apiKey
            });

            if (!result.Succeeded)
            {
                return StatusCode((int?)result.ErrorType ?? 400, new ApiResponse
                {
                    Messages = result.ErrorMessages
                });
            }

            return NoContent();
        }
    }
}