using System.Collections.Generic;
using System.Threading.Tasks;
using KiedyKolos.Core.Models;

namespace KiedyKolos.Core.Interfaces
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
    }
}