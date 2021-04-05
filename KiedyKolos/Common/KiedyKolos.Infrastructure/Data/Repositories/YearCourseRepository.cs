using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace KiedyKolos.Infrastructure.Data.Repositories
{
    public class YearCourseRepository : IYearCourseRepository
    {
        private readonly AppDbContext _dbContext;
        public YearCourseRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task AddAsync(YearCourse yearCourse)
        {
            _dbContext.YearCourses.Add(yearCourse);
        }

        public async Task DeleteAsync(int id)
        {
            var yearCourse = await _dbContext.YearCourses.FindAsync(id);

            if (yearCourse == null)
            {
                return;
            }

            _dbContext.Entry(yearCourse).State = EntityState.Deleted;
            _dbContext.YearCourses.Remove(yearCourse);
        }

        public async Task<YearCourse> GetAsync(int id)
        {
            return await _dbContext.YearCourses.FindAsync(id);
        }

        public async Task<YearCourse> GetAsyncWithGroups(int id)
        {
            return await _dbContext.YearCourses
                .Include(x => x.Groups)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async  Task<List<YearCourse>> GetAllAsync()
        {
            return await _dbContext.YearCourses.ToListAsync();
        }

        public async Task UpdateAsync(YearCourse yearCourse)
        {
            var existingYearCourse = await _dbContext.YearCourses.FindAsync(yearCourse.Id);
            _dbContext.Entry(existingYearCourse).CurrentValues.SetValues(yearCourse);
        }
    }
}