using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAll();
        Task<T> Get(int id);
        Task Insert(T entity);
        Task Delete(int id);
        Task Update(T entity);
    }
}