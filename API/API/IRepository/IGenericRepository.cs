using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>,IOrderedQueryable<T>> orderby = null,
            List<string> includes = null
            );
        Task<T> Get(Expression<Func<T,bool>> expression,List<string> includes = null);
        Task Insert(T entity);
        Task Delete(int id);
        public void Update(T entity);
    }
}