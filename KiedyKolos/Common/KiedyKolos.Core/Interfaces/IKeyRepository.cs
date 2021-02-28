using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiedyKolos.Core.Interfaces
{
    public interface IKeyRepository
    {
        Task AddAsync(string key);
        Task<bool> TryUseKeyAsync(string key);
    }
}
