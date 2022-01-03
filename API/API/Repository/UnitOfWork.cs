using API.Data;
using API.IRepository;
using System;
using System.Threading.Tasks;

namespace API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;
        private IGenericRepository<Character> _characters;
        private IGenericRepository<Movie> _movies;
        private IGenericRepository<CharactersAndMovies> _moviesandcharacters;
        private IMovieRepository<Movie> _movie;
        private ICharacterRepository<Character> _character;
        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }
        public IGenericRepository<Character> Characters => _characters ??= new GenericRepository<Character>(_context);
        public IGenericRepository<Movie> Movies => _movies ??= new GenericRepository<Movie>(_context);

        public IMovieRepository<Movie> Movie => _movie ??= new MovieRepository<Movie>(_context);

        public ICharacterRepository<Character> Character => _character ??= new CharacterRepository<Character>(_context);

        public IGenericRepository<CharactersAndMovies> CharactersAndMovies => _moviesandcharacters ??= new GenericRepository<CharactersAndMovies>(_context);

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
