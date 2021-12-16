using API.Data;
using API.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
    public class MovieRepository : IMovieRepository
    {
        public Task<IEnumerable<Movie>> GetAllByInformation(string name, int idGender, string orderBy)
        {
            throw new System.NotImplementedException();
        }
    }
}
