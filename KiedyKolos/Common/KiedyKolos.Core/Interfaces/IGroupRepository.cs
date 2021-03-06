using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiedyKolos.Core.Models;

namespace KiedyKolos.Core.Interfaces
{
    public interface IGroupRepository
    {
        Task AddAsync(Group group);
        Task<List<Group>> GetAllForYearCourseAsync(int yearCourseId);
        Task<Group> GetByIdAsync(int id);
        Task UpdateAsync(Group group);
        Task DeleteAsync(int id);
        Task<Group> GetYearCourseGroup(int yearCourseId, int groupId);
    }
}
