using API.Configurations.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CharactersAndMovies> CharactersAndMovies { get; set; }

        //datos que gregare en la base de datos desde una migracion
        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new GenderConfiguration());
        }
    }
}
