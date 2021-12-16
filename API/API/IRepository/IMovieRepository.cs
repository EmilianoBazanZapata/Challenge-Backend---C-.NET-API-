using API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.IRepository
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllByInformation(string name,int idGender,string orderBy);
    }
}
