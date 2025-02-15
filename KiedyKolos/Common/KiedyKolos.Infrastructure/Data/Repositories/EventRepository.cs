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
            {
                return;
            }

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

        public async Task<Event> GetYearCourseEventWithGroupsAsync(int yearCourseId, int id)
        {
            return await _dbContext.Events.Where(e => e.YearCourseId == yearCourseId).Include(e => e.GroupEvents).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Event>> GetYearCourseEventAsync(int yearCourseId, DateTime? date, List<int> groupIds)
        {
            var events = _dbContext.Events.Where(e => e.YearCourseId == yearCourseId);
            if (date != null)
                events = events.Where(e => e.Date.Date == ((DateTime)date).Date);
            if (groupIds != null && groupIds.Count > 0)
                events = events.Where(x => x.GroupEvents.Any(x => groupIds.Contains(x.Id)));
            return await events.ToListAsync();
        }

        public async Task<List<Event>> GetYearCourseEventsForGroupAsync(int yearCourseId, int groupId, DateTime? date)
        {
            var eventIds = _dbContext.GroupEvents.Where(x => x.GroupId == groupId).Select(y => y.EventId);
            var events = _dbContext.Events.Where(e => e.YearCourseId == yearCourseId && eventIds.Contains(e.Id));
            if (date != null)
                return await events.Where(e => e.Date.Date == ((DateTime)date).Date).ToListAsync();
            return await events.ToListAsync();
        }

        public async Task UpdateAsync(Event eventToUpdate)
        {
            var existingEvent = await _dbContext.Events.FindAsync(eventToUpdate.Id);
            _dbContext.Entry(existingEvent).CurrentValues.SetValues(eventToUpdate);
        }
    }
}