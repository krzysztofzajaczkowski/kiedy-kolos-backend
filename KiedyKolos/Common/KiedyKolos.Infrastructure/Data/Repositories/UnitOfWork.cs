using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Infrastructure.Data.Context;

namespace KiedyKolos.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private IYearCourseRepository _yearCourseRepository;
        private KeyRepository _keyRepository;
        private SubjectRepository _subjectRepository;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IYearCourseRepository YearCourseRepository => _yearCourseRepository ??= new YearCourseRepository(_appDbContext);

        public IKeyRepository KeyRepository => _keyRepository ??= new KeyRepository(_appDbContext);
        public ISubjectRepository SubjectRepository => _subjectRepository ??= new SubjectRepository(_appDbContext);

        public async Task CommitAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            await _appDbContext.DisposeAsync();
        }
    }
}
