using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace KiedyKolos.Infrastructure.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _dbContext;

        public EventRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Event eventToAdd)
        {
            _dbContext.Events.Add(eventToAdd);
        }

        public async Task DeleteAsync(int id)
        {
            var eventToDelete = await _dbContext.Events.FindAsync(id);

            if (eventToDelete == null)
                return;

            _dbContext.Entry(eventToDelete).State = EntityState.Deleted;
            _dbContext.Events.Remove(eventToDelete);
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _dbContext.Events.ToListAsync();
        }

        public async Task<Event> GetAsync(int id)
        {
            return await _dbContext.Events.FindAsync(id);
        }

        public async Task<List<Event>> GetYearCourseEventAsync(int yearCourseId, DateTime? date)
        {
            var events = _dbContext.Events.Where(e => e.YearCourseId == yearCourseId);
            if(date != null)
                return await events.Where(e => e.Date == date).ToListAsync();
            return await events.ToListAsync();
        }
    }
}