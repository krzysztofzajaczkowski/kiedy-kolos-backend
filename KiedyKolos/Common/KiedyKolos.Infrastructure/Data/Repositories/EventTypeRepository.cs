using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace KiedyKolos.Infrastructure.Data.Repositories
{
    public class EventTypeRepository : IEventTypeRepository
    {
        private readonly AppDbContext _dbContext;

        public EventTypeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(EventType eventType)
        {
            _dbContext.EventTypes.Add(eventType);
        }

        public async Task<List<EventType>> GetAllAsync()
        {
            var eventTypes = await _dbContext.EventTypes.ToListAsync();
            return eventTypes;
        }
    }
}
