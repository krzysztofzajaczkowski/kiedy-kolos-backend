using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiedyKolos.Core.Models;

namespace KiedyKolos.Core.Interfaces
{
    public interface ISubjectRepository
    {
        Task AddAsync(Subject subject);
        Task<List<Subject>> GetAllForYearCourseAsync(int yearCourseId);
        Task<Subject> GetByIdAsync(int id);
        Task UpdateAsync(Subject subject);
        Task DeleteAsync(int id);
        Task<Subject> GetYearCourseSubject(int yearCourseId, int subjectId);
    }
}
