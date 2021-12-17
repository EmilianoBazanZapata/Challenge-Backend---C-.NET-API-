using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class CharactersAndMovies
    {
        [Key]
        public int IdCharactersAndMovies { get; set; }
        [ForeignKey(nameof(Character))]
        public int IdCharacter { get; set; }
        public Character Character { get; set; }
        [ForeignKey(nameof(Movie))]
        public int IdMovie { get; set; }
        public Movie Movie { get; set; }
    }
}
