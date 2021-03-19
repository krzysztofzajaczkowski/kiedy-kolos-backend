using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KiedyKolos.Api.Configuration;
using KiedyKolos.Api.Requests;
using KiedyKolos.Api.Responses;
using KiedyKolos.Core.Commands;
using KiedyKolos.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KiedyKolos.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventTypesController : ControllerBase
    {
        private AuthOptions _options;
        private IMediator _mediator;
        private IMapper _mapper;

        public EventTypesController(IOptions<AuthOptions> options, IMediator mediator, IMapper mapper)
        {
            _options = options.Value;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("/yearCourses/{yearCourseId}/[controller]")]
        public async Task<IActionResult> AddAsync(int yearCourseId, CreateEventTypeRequest request)
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

            var result = await _mediator.Send(new CreateEventTypeCommand
            {
                YearCourseId = request.YearCourseId,
                Password = apiKey,
                Name = request.Name
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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new GetAllEventTypesQuery());

            if (!result.Succeeded)
            {
                return StatusCode((int?)result.ErrorType ?? 400, new ApiResponse
                {
                    Messages = result.ErrorMessages
                });
            }

            var dto = _mapper.Map<List<GetEventTypeResponse>>(result.Output);

            return Ok(new ApiResponse<List<GetEventTypeResponse>>
            {
                Result = dto
            });
        }

    }
}
