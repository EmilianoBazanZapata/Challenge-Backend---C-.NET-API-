using API.Data;
using API.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        public Task<IEnumerable<Character>> GetByInformation(string name, int age, int idMovie)
        {
            throw new System.NotImplementedException();
        }
    }
}
