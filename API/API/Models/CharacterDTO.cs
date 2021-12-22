using API.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CreateCharacterDTO
    {
        [Required(ErrorMessage = "The Image Name is Required")]
        [MaxLength(1000, ErrorMessage = "Gender Name Is Too Long")]
        [MinLength(2, ErrorMessage = "Gender Name Is Too Short")]
        public string Image { get; set; }
        [Required(ErrorMessage = "The Name is Required")]
        [MaxLength(250, ErrorMessage = "Character Name Is Too Long")]
        [MinLength(2, ErrorMessage = "Character Name Is Too Short")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Last Name is Required")]
        [MaxLength(250, ErrorMessage = "Character Last Name Is Too Long")]
        [MinLength(2, ErrorMessage = "Character Last Name Is Too Short")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Age is Required")]
        [Range(1, 110)]
        public int Age { get; set; }
        [Required(ErrorMessage = "Weight is Required")]
        [Range(1, 999)]
        public float Weight { get; set; }
        [Required(ErrorMessage = "The History is Required")]
        [MaxLength(5000, ErrorMessage = "Character History Is Too Long")]
        public string History { get; set; }
    }
    public class CharacterDTO : CreateCharacterDTO
    {
        public int IdCaharacter { get; set; }
    }
}
