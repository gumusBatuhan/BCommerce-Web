using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.UOWAndRepo
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);
        IList<T> GetAll();
        IList<T> GetAll(Func<T, bool> predicate);
        void Add(T entity);

        void Remove(T entity);
    }
}
