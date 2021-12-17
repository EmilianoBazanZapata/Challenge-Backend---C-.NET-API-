using API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.IRepository
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetByName(string name);
        Task<IEnumerable<Character>> GetByAge(int? age);
        Task<IEnumerable<Character>> GetByIdMovie(int? idMovie);

    }
}
