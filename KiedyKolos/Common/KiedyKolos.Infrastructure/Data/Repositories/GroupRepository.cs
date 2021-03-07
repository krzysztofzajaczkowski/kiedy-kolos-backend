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
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _dbContext;

        public GroupRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Group group)
        {
            _dbContext.Groups.Add(group);
        }

        public async Task<List<Group>> GetAllForYearCourseAsync(int yearCourseId)
        {
            var yearCourse = await _dbContext.YearCourses.Include(yc => yc.Groups).FirstOrDefaultAsync(yc => yc.Id == yearCourseId);

            return yearCourse?.Groups.ToList();
        }

        public async Task<Group> GetByIdAsync(int id)
        {
            return await _dbContext.Groups.FindAsync(id);
        }

        public async Task UpdateAsync(Group group)
        {
            var existingGroup = await _dbContext.Groups.FindAsync(group.Id);
            _dbContext.Entry(existingGroup).CurrentValues.SetValues(group);
        }

        public async Task DeleteAsync(int id)
        {

            var group = await _dbContext.Groups.FindAsync(id);

            if (group == null)
            {
                return;
            }

            _dbContext.Entry(group).State = EntityState.Deleted;
            _dbContext.Groups.Remove(group);
        }

        public async Task<Group> GetYearCourseGroup(int yearCourseId, int groupId)
        {
            return await _dbContext.Groups.FirstOrDefaultAsync(s => s.Id == groupId && s.YearCourseId == yearCourseId);
        }
    }
}
