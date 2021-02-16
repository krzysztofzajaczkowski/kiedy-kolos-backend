using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KiedyKolos.Core.Models;
using KiedyKolos.Infrastructure.Data.Context;

namespace KiedyKolos.Infrastructure.Data.Repositories
{
    public class YearCourseRepository : IYearCourseRepository
    {
        private readonly AppDbContext _dbContext;
        public YearCourseRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task Add(YearCourse yearCourse)
        {
            _dbContext.YearCourses.Add(yearCourse);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var yearCourse = await _dbContext.YearCourses.FindAsync(id);
            if(yearCourse == null)
                return;
            _dbContext.YearCourses.Remove(yearCourse);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<YearCourse> Get(int id)
        {
            return await _dbContext.YearCourses.FindAsync(id);
        }

        public List<YearCourse> GetAll()
        {
            return _dbContext.YearCourses.ToList();
        }

        public async Task Update(YearCourse yearCourse)
        {
            _dbContext.YearCourses.Update(yearCourse);
            await _dbContext.SaveChangesAsync();
        }
    }
}