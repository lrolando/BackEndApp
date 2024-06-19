using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task<T> GetByEntity(Func<T, bool> predicate);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
