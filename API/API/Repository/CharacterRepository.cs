using API.Data;
using API.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataBaseContext _context;
        public CharacterRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Character>> GetByAge(int? age)
        {
            var result = await _context.Characters.Where(x => x.Age == age).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Character>> GetByIdMovie(int? idMovie)
        {
            var query = (from Per in _context.Characters
                         join PP in _context.CharactersAndMovies on Per.IdCaharacter equals PP.IdCharacter
                         join Ps in _context.Movies on PP.IdMovie equals Ps.IdMovie
                         where PP.IdMovie == idMovie
                         select Per);
            var lista = await query.ToListAsync();
            return lista;
        }

        public async Task<IEnumerable<Character>> GetByName(string name)
        {
            var result = await _context.Characters.Where(x => x.Name == name).ToListAsync();
            return result;
        }
    }
}
