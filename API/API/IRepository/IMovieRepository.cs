using API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.IRepository
{
    public interface IMovieRepository<T> where T : class
    {
        Task<IEnumerable<Movie>> GetAllByGender(int? gender);
        Task<IEnumerable<Movie>> GetAllAndOrderBy(string order);
    }
}