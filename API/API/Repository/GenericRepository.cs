using API.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<T>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task Insert(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
