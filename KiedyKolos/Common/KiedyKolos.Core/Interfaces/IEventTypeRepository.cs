using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiedyKolos.Core.Models;

namespace KiedyKolos.Core.Interfaces
{
    public interface IEventTypeRepository
    {
        Task AddAsync(EventType eventType);
        Task<List<EventType>> GetAllAsync();
    }
}
