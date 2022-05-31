using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DataAccessLayer.RepositotyPatterns
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _Context;
        public async Task<int> Complete()
        {
            return await _Context.SaveChangesAsync();
        }

        public UnitOfWork(DbContext Context)
        {
            _Context = Context;

        }
    }
}
