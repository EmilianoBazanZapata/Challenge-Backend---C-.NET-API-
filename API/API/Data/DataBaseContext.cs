using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Characters> Characters { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieAndCaharacter> MovieAndCaharacters { get; set; }
    }
}
