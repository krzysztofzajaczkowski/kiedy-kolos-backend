using System;
using System.Threading.Tasks;
using KiedyKolos.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace KiedyKolos.Api.Controllers
{
    [ApiController]
    [Route("yearCourses/{yearCourseId}/[controller]")]
    public class EventsController : ControllerBase
    {
        public EventsController()
        {

        }

        [HttpGet]
        [Route("/[controller]")]
        public async Task<IActionResult> GetAllAsync()
        {
            return null;
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