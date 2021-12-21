using API.Data;
using System;
using System.Threading.Tasks;

namespace API.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Character> Characters { get; }
        IGenericRepository<Movie> Movies { get; }
        IMovieRepository<Movie> Movie { get; }
        Task Save();
    }
}
