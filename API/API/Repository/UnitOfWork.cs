using API.Data;
using API.IRepository;
using System;
using System.Threading.Tasks;

namespace API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;
        private IGenericRepository<Character> _character;
        private IGenericRepository<Movie> _movie;

        public IGenericRepository<Character> Characters => _character ??= new GenericRepository<Character>(_context);
        public IGenericRepository<Movie> Movies => _movie ??= new GenericRepository<Movie>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
