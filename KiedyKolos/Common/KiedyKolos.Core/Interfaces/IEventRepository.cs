using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KiedyKolos.Core.Models;

namespace KiedyKolos.Core.Interfaces
{
    public interface IEventRepository
    {
        Task AddAsync(Event eventToAdd);
        Task<Event> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<List<Event>> GetAllAsync();
        Task<List<Event>> GetYearCourseEventAsync(int yearCourseId, DateTime? date);
    }
}