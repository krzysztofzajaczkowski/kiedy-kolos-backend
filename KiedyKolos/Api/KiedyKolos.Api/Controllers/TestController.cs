using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KiedyKolos.Api.Configuration;
using KiedyKolos.Api.Requests;
using KiedyKolos.Api.Responses;
using KiedyKolos.Core.Result;
using KiedyKolos.Core.Commands;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KiedyKolos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly AuthOptions _options;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestController(IOptions<AuthOptions> options, IMediator mediator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _options = options.Value;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var yearCourses = await _unitOfWork.YearCourseRepository.GetAllAsync();
            var dto = _mapper.Map<List<GetBlockYearCourseResponse>>(yearCourses);
            return Ok(new ApiResponse<List<GetBlockYearCourseResponse>>
            {
                Result = dto
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(id);
            var dto = _mapper.Map<GetYearCourseResponse>(yearCourse);
            return Ok(new ApiResponse<GetYearCourseResponse>
            {
                Result = dto
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _unitOfWork.YearCourseRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Add(CreateYearCourseRequest request)
        {
            var yearCourse = _mapper.Map<YearCourse>(request);
            await _unitOfWork.YearCourseRepository.AddAsync(yearCourse);
            await _unitOfWork.CommitAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateYearCourseRequest request)
        {
            var yearCourse = _mapper.Map<YearCourse>(request);
            await _unitOfWork.YearCourseRepository.UpdateAsync(yearCourse);
            await _unitOfWork.CommitAsync();
            
            return Ok();
        }
    }
}