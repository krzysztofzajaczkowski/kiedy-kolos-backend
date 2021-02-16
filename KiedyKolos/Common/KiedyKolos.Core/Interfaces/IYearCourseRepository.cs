using System.Collections.Generic;
using System.Threading.Tasks;
using KiedyKolos.Core.Models;

namespace KiedyKolos.Core.Interfaces
{
    public interface IYearCourseRepository
    {
        Task<List<YearCourse>> GetAllAsync();
        Task<YearCourse> GetAsync(int id);
        Task AddAsync(YearCourse yearCourse);
        Task DeleteAsync(int id);
        Task UpdateAsync(YearCourse yearCourse);
    }
}