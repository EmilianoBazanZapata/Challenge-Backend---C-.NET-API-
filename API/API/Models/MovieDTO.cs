using API.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class CreateMovieDTO
    {
        [Required(ErrorMessage = "The Image Name is Required")]
        [MaxLength(1000, ErrorMessage = "Image Name Is Too Long")]
        [MinLength(2, ErrorMessage = "Image Name Is Too Short")]
        public string Image { get; set; }
        [Required(ErrorMessage = "The Title is Required")]
        [MaxLength(250, ErrorMessage = "The Title Is Too Long")]
        [MinLength(2, ErrorMessage = "The Title Is Too Short")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The Data Creation is Required")]
        public DateTime DataCreation { get; set; }
        [Required(ErrorMessage = "Qualification is Required")]
        [Range(1, 5)]
        public int Qualification { get; set; }
        public int IdGender { get; set; }
    }
    public class MovieDTO : CreateMovieDTO
    {
        public int Id { get; set; }
        public IList<Character> Characters { get; set; }
    }
}