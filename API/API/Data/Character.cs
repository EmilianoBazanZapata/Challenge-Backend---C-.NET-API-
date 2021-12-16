using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class Character
    {
        public Character()
        {
            this.Movies = new HashSet<Movie>();
        }
        [Key]
        public int IdCaharacter { get; set; }
        [Required(ErrorMessage = "The Image Name is Required")]
        [StringLength(maximumLength: 1000, ErrorMessage = "Gender Name Is Too Long")]
        public string Image { get; set; }
        [Required(ErrorMessage = "The Name is Required")]
        [StringLength(maximumLength: 250, ErrorMessage = "Character Name Is Too Long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Last Name is Required")]
        [StringLength(maximumLength: 250, ErrorMessage = "Character Last Name Is Too Long")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Age is Required")]
        [Range(1, 110)]
        public int Age { get; set; }
        [Required(ErrorMessage = "Weight is Required")]
        [Range(1, 100)]
        public float Weight { get; set; }
        [Required(ErrorMessage = "The History is Required")]
        [StringLength(maximumLength: 5000, ErrorMessage = "Character History Is Too Long")]
        public string History { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
