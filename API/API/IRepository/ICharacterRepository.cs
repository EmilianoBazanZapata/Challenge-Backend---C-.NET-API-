using API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.IRepository
{
    public interface ICharacterRepository<T> where T : class
    {
        Task<IEnumerable<Character>> GetByIdMovie(int? idMovie);
    }
}
