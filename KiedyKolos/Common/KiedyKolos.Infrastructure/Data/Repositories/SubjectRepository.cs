using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace KiedyKolos.Infrastructure.Data.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _dbContext;

        public SubjectRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Subject subject)
        {
            _dbContext.Subjects.Add(subject);
        }

        public async Task<List<Subject>> GetAllForYearCourseAsync(int yearCourseId)
        {
            var yearCourse = await _dbContext.YearCourses.Include(yc => yc.Subjects).FirstOrDefaultAsync(yc => yc.Id == yearCourseId);

            return yearCourse?.Subjects.ToList();
        }

        public async Task<Subject> GetByIdAsync(int id)
        {
            return await _dbContext.Subjects.FindAsync(id);
        }

        public async Task UpdateAsync(Subject subject)
        {
            var existingSubject = await _dbContext.Subjects.FindAsync(subject.Id);
            _dbContext.Entry(existingSubject).CurrentValues.SetValues(subject);
        }

        public async Task DeleteAsync(int id)
        {
            //var subject = new Subject
            //{
            //    Id = id
            //};

            var subject = await _dbContext.Subjects.FindAsync(id);

            if (subject == null)
            {
                return;
            }

            //if (!await _dbContext.Subjects.ContainsAsync(subject))
            //{
            //    return;
            //}

            _dbContext.Entry(subject).State = EntityState.Deleted;
            _dbContext.Subjects.Remove(subject);
        }

        public async Task<Subject> GetYearCourseSubject(int yearCourseId, int subjectId)
        {
            return await _dbContext.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId && s.YearCourseId == yearCourseId);
        }
    }
}
