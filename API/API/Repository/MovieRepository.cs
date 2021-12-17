using API.Data;
using API.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataBaseContext _context;
        public MovieRepository(DataBaseContext context)
        {
            _context = context;
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
            var result = await _context.Movies.Where(x => x.Gender.Id == gender).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Movie>> GetAllByName(string name)
        {
            var result = await _context.Movies.Where(x => x.Title == name).ToListAsync();
            return result;
        }
    }
}