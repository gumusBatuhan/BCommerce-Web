using BCommerce.Application.Data;
using BCommerce.Application.UOWAndRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Insfrastructure.UOWAndRepo
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IApplicationDbContext _dbContext;
        private bool _disposed;
        public UnitOfWork(IApplicationDbContext dbContext)
        {

            _dbContext = dbContext;
        }
        public void Commit()
        {
            _dbContext.CurentContext.SaveChanges();
        }
        public void Rollback()
        {
            foreach (var entry in _dbContext.CurentContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
        public IRepository<T> Repository<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.CurentContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
    }
}
