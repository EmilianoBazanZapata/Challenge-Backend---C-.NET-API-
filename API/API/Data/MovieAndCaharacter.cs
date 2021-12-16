using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class MovieAndCaharacter
    {
        [Key]
        public int IdMovieAndCharacter { get; set; }
        public int IdCaharacter { get; set; }
        public int IdMovie { get; set; }

        [ForeignKey("IdCaharacter")]
        public virtual ICollection<Characters> Characters { get; set; }
        [ForeignKey("IdMovie")]
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
