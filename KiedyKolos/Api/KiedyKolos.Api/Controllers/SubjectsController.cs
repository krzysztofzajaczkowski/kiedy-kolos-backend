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
    [Route("yearCourses/{yearCourseId}/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly AuthOptions _options;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SubjectsController(IOptions<AuthOptions> options, IMediator mediator, IMapper mapper)
        {
            _options = options.Value;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(int yearCourseId, CreateSubjectRequest request)
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

            var result = await _mediator.Send(new CreateYearCourseSubjectCommand
            {
                YearCourseId = request.YearCourseId,
                Password = apiKey,
                Name = request.Name,
                ShortName = request.ShortName
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
            var result = await _mediator.Send(new GetAllYearCourseSubjectsQuery
            {
                YearCourseId = yearCourseId
            });

            if (!result.Succeeded)
            {
                return StatusCode((int?) result.ErrorType ?? 400, new ApiResponse
                {
                    Messages = result.ErrorMessages
                });
            }

            var dto = _mapper.Map<List<GetSubjectResponse>>(result.Output);

            return Ok(new ApiResponse<List<GetSubjectResponse>>
            {
                Result = dto
            });
        }

        [HttpGet("{subjectId}")]
        public async Task<IActionResult> GetAsync(int yearCourseId, int subjectId)
        {
            var result = await _mediator.Send(new GetYearCourseSubjectByIdQuery
            {
                YearCourseId = yearCourseId,
                SubjectId = subjectId
            });

            if (!result.Succeeded)
            {
                return StatusCode((int?)result.ErrorType ?? 400, new ApiResponse
                {
                    Messages = result.ErrorMessages
                });
            }

            var dto = _mapper.Map<GetSubjectResponse>(result.Output);

            return Ok(new ApiResponse<GetSubjectResponse>
            {
                Result = dto
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int yearCourseId, UpdateSubjectRequest request)
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

            var result = await _mediator.Send(new UpdateYearCourseSubjectCommand
            {
                YearCourseId = yearCourseId,
                Password = apiKey,
                SubjectId = request.SubjectId,
                Name = request.Name,
                ShortName = request.ShortName
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

        [HttpDelete("{subjectId}")]
        public async Task<IActionResult> DeleteAsync(int yearCourseId, int subjectId)
        {
            var apiKey = Request.Headers.FirstOrDefault(h => string.Equals(h.Key, _options.ApiKeyHeaderName, StringComparison.CurrentCultureIgnoreCase))
                .Value;

            var result = await _mediator.Send(new DeleteYearCourseSubjectCommand
            {
                YearCourseId = yearCourseId,
                Password = apiKey,
                SubjectId = subjectId

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
