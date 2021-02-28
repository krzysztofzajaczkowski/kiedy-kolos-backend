using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace KiedyKolos.Infrastructure.Data.Repositories
{
    public class KeyRepository : IKeyRepository
    {
        private readonly AppDbContext _dbContext;

        public KeyRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(string key)
        {
            await _dbContext.Keys.AddAsync(new Key
            {
                Value = key
            });
        }

        public async Task<bool> TryUseKeyAsync(string key)
        {
            var existingKey = await _dbContext.Keys.FirstOrDefaultAsync(k => k.Value == key);
            
            if (existingKey == null)
            {
                return false;
            }

            _dbContext.Keys.Remove(existingKey);

            return true;
        }
    }
}
