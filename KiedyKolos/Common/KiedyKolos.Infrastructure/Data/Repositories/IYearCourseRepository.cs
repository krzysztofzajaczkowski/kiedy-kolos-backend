using System.Collections.Generic;
using System.Threading.Tasks;
using KiedyKolos.Core.Models;

namespace KiedyKolos.Infrastructure.Data.Repositories
{
    public interface IYearCourseRepository
    {
        List<YearCourse> GetAll();
        Task<YearCourse> Get(int id);
        Task Add(YearCourse yearCourse);
        Task Delete(int id);
        Task Update(YearCourse yearCourse);
    }
}