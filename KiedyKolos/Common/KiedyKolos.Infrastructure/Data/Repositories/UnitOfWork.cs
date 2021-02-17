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

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IYearCourseRepository YearCourseRepository => _yearCourseRepository ??= new YearCourseRepository(_appDbContext);

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
