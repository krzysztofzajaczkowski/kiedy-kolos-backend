using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KiedyKolos.Api.Configuration;
using KiedyKolos.Api.Requests;
using KiedyKolos.Api.Responses;
using KiedyKolos.Core.Commands;
using KiedyKolos.Core.Dtos;
using KiedyKolos.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KiedyKolos.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class YearCoursesController : ControllerBase
    {
        private readonly AuthOptions _options;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public YearCoursesController(IOptions<AuthOptions> options, IMediator mediator, IMapper mapper)
        {
            _options = options.Value;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new GetAllYearCoursesQuery());
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse<List<GetBlockYearCourseResponse>>
                {
                    Messages = result.ErrorMessages
                });
            }

            var dto = _mapper.Map<List<GetBlockYearCourseResponse>>(result.Output);
            return Ok(new ApiResponse<List<GetBlockYearCourseResponse>>
            {
                Result = dto
            });
        }

        [HttpPost("auth")]
        public async Task<IActionResult> AuthAsync(AuthRequest request)
        {
            var result = await _mediator.Send(new YearCourseAuthQuery
            {
                YearCourseId = request.YearCourseId,
                Password = request.Password
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _mediator.Send(new GetYearCourseByIdQuery
            {
                YearCourseId = id
            });

            if (!result.Succeeded)
            {
                return StatusCode((int?) result.ErrorType ?? 400, new ApiResponse<GetYearCourseResponse>
                {
                    Messages = result.ErrorMessages
                });
            }

            var dto = _mapper.Map<GetYearCourseResponse>(result.Output);
            return Ok(new ApiResponse<GetYearCourseResponse>
            {
                Result = dto
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateYearCourseRequest request)
        {
            var result = await _mediator.Send(new CreateYearCourseCommand
            {
                Password = request.Password,
                CourseStartYear = request.CourseStartYear,
                Faculty = request.Faculty,
                University = request.University,
                CreationApiKey = request.CreationApiKey,
                CurrentSemester = request.CurrentSemester,
                Course = request.Course,
                Subjects = _mapper.Map<List<CreateSubjectDto>>(request.Subjects),
                Groups = _mapper.Map<List<CreateGroupDto>>(request.Groups)
            });

            if (!result.Succeeded)
            {
                return StatusCode((int?) result.ErrorType ?? 400, new ApiResponse<int>
                {
                    Messages = result.ErrorMessages
                });
            }

            return Ok(new ApiResponse<int>
            {
                Result = result.Output
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateYearCourseRequest request)
        {
            var apiKey = Request.Headers.FirstOrDefault(h => string.Equals(h.Key, _options.ApiKeyHeaderName, StringComparison.CurrentCultureIgnoreCase))
                .Value;
            
            var result = await _mediator.Send(new UpdateYearCourseCommand
            {
                Password = apiKey,
                CurrentSemester = request.CurrentSemester,
                Course = request.Course,
                CourseStartYear = request.CourseStartYear,
                Faculty = request.Faculty,
                University = request.University,
                NewPassword = request.NewPassword,
                YearCourseId = request.YearCourseId
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var apiKey = Request.Headers.FirstOrDefault(h => string.Equals(h.Key, _options.ApiKeyHeaderName, StringComparison.CurrentCultureIgnoreCase))
                .Value;

            var result = await _mediator.Send(new DeleteYearCourseCommand
            {
                Password = apiKey,
                YearCourseId = id
            });

            if (!result.Succeeded)
            {
                return StatusCode((int?)result.ErrorType ?? 400, new ApiResponse<int>
                {
                    Messages = result.ErrorMessages
                });
            }

            return NoContent();
        }
    }
}
