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
        public ActionResult GetAll()
        {
            return Ok(_yearCourseRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_yearCourseRepository.Get(id));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_yearCourseRepository.Delete(id));
        }

        [HttpPost]
        public ActionResult Add(YearCourse yearCourse)
        {
            _yearCourseRepository.Add(yearCourse);

            return Ok();
        }

        [HttpPatch]
        public ActionResult Update(YearCourse yearCourse)
        {
            _yearCourseRepository.Update(yearCourse);
            return Ok();
        }
    }
}