using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KiedyKolos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IYearCourseRepository _yearCourseRepository;
        public TestController(IYearCourseRepository yearCourseRepository)
        {
            _yearCourseRepository = yearCourseRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _yearCourseRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _yearCourseRepository.GetAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _yearCourseRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Add(YearCourse yearCourse)
        {
            await _yearCourseRepository.AddAsync(yearCourse);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, YearCourse yearCourse)
        {
            await _yearCourseRepository.UpdateAsync(yearCourse);
            return Ok();
        }
    }
}