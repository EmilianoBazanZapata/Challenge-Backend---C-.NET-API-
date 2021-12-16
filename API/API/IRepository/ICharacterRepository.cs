using API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.IRepository
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetByInformation(string name,int age, int idMovie);
    }
}
