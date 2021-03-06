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
    public class GroupsController : ControllerBase
    {
        private AuthOptions _options;
        private IMediator _mediator;
        private IMapper _mapper;

        public GroupsController(IOptions<AuthOptions> options, IMediator mediator, IMapper mapper)
        {
            _options = options.Value;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(int yearCourseId, CreateGroupRequest request)
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

            var result = await _mediator.Send(new CreateYearCourseGroupCommand
            {
                YearCourseId = request.YearCourseId,
                Password = apiKey,
                GroupName = request.GroupName,
                GroupNumber = request.GroupNumber
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
        public async Task<IActionResult> GetAllAsync(int yearCourseId)
        {
            var result = await _mediator.Send(new GetAllYearCourseGroupsQuery
            {
                YearCourseId = yearCourseId
            });

            if (!result.Succeeded)
            {
                return StatusCode((int?)result.ErrorType ?? 400, new ApiResponse
                {
                    Messages = result.ErrorMessages
                });
            }

            var dto = _mapper.Map<List<GetGroupResponse>>(result.Output);

            return Ok(new ApiResponse<List<GetGroupResponse>>
            {
                Result = dto
            });
        }

        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetAsync(int yearCourseId, int groupId)
        {
            var result = await _mediator.Send(new GetYearCourseGroupByIdQuery
            {
                YearCourseId = yearCourseId,
                GroupId = groupId
            });

            if (!result.Succeeded)
            {
                return StatusCode((int?)result.ErrorType ?? 400, new ApiResponse
                {
                    Messages = result.ErrorMessages
                });
            }

            var dto = _mapper.Map<GetGroupResponse>(result.Output);

            return Ok(new ApiResponse<GetGroupResponse>
            {
                Result = dto
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int yearCourseId, UpdateGroupRequest request)
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

            var result = await _mediator.Send(new UpdateYearCourseGroupCommand
            {
                YearCourseId = yearCourseId,
                Password = apiKey,
                GroupId = request.GroupId,
                GroupName = request.GroupName,
                GroupNumber = request.GroupNumber
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

        [HttpDelete("{groupId}")]
        public async Task<IActionResult> DeleteAsync(int yearCourseId, int groupId)
        {
            var apiKey = Request.Headers.FirstOrDefault(h => string.Equals(h.Key, _options.ApiKeyHeaderName, StringComparison.CurrentCultureIgnoreCase))
                .Value;

            var result = await _mediator.Send(new DeleteYearCourseGroupCommand
            {
                YearCourseId = yearCourseId,
                Password = apiKey,
                GroupId = groupId

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
