using API.Data;
using API.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class MovieRepository<T> : IMovieRepository<T> where T : class
    {
        private readonly DataBaseContext _context;
        private readonly DbSet<T> _db;
        public MovieRepository(DataBaseContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }
        public async Task<IEnumerable<Movie>> GetAllAndOrderBy(string order)
        {
            var result = _context.Movies.ToList();
            switch (order)
            {
                case "ASC":
                    result = await _context.Movies.OrderBy(x => x.DataCreation).ToListAsync();
                    break;
                case "DESC":
                    result = await _context.Movies.OrderByDescending(x => x.DataCreation).ToListAsync();
                    break;
                default:
                    result = await _context.Movies.OrderBy(x => x.DataCreation).ToListAsync();
                    break;
            }
            return result;
        }

        public async Task<IEnumerable<Movie>> GetAllByGender(int? gender)
        {
            var result = await _context.Movies.Where(x => x.Gender.IdGender == gender).ToListAsync();
            return result;
        }
    }
}