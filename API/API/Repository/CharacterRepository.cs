using API.Data;
using API.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class CharacterRepository<T> : ICharacterRepository<T> where T : class
    {
        private readonly DataBaseContext _context;
        private readonly DbSet<T> _db;
        public CharacterRepository(DataBaseContext context)
        {
            _context = context;
            _db = _context.Set<T>();
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
    }
}
