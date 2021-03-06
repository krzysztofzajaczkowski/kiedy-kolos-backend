using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KiedyKolos.Api.Configuration;
using KiedyKolos.Api.Responses;
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
                return StatusCode((int?) result.ErrorType ?? 400, new ApiResponse
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
        public async Task<IActionResult> GetAllCourseEventsAsync(int yearCourseId, [FromQuery] DateTime date)
        {
            return null;
        }

        [HttpGet("/yearCourses/{yearCourseId}/groups/{groupId}/[controller]")]
        public async Task<IActionResult> GetYearCourseEventsForGroupAsync(int yearCourseId, int groupId, [FromQuery] DateTime date)
        {
            return null;
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventDetailsAsync(int yearCourseId, int eventId)
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> AddEventToYearCourseAsync(Event newEvent)
        {
            return null;
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateEventDetailsAsync(int eventId, Event newEvent)
        {
            return null;
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEventAsync(int eventId)
        {
            return null;
        }
    }
}