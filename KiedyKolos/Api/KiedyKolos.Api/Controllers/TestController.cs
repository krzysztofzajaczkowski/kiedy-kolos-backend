using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace KiedyKolos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _unitOfWork.YearCourseRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _unitOfWork.YearCourseRepository.GetAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _unitOfWork.YearCourseRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Add(YearCourse yearCourse)
        {
            await _unitOfWork.YearCourseRepository.AddAsync(yearCourse);
            await _unitOfWork.CommitAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, YearCourse yearCourse)
        {
            await _unitOfWork.YearCourseRepository.UpdateAsync(yearCourse);
            await _unitOfWork.CommitAsync();
            
            return Ok();
        }
    }
}