using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiedyKolos.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IYearCourseRepository YearCourseRepository { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}
