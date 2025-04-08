using BCommerce.Application.Data;
using BCommerce.Application.UOWAndRepo;
using BCommerce.Core.DomainClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Insfrastructure.UOWAndRepo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }
        public Repository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext.CurentContext;
            _dbSet = _dbContext.Set<T>();
        }

   

        public IList<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IList<T> GetAll(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }


        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
