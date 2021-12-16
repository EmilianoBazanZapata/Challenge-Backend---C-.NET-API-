﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class Movie
    {
        public Movie() 
        {
            this.Characters = new HashSet<Character>();
        }
        [Key]
        public int IdMovie { get; set; }
        [Required(ErrorMessage = "The Image Name is Required")]
        [StringLength(maximumLength: 1000, ErrorMessage = "Gender Name Is Too Long")]
        public string Image { get; set; }
        [Required(ErrorMessage = "The Title is Required")]
        [StringLength(maximumLength: 250, ErrorMessage = "The Title Is Too Long")]
        public string  Title { get; set; }
        [Required(ErrorMessage = "The Data Creation is Required")]
        public DateTime DataCreation { get; set; }
        [Required(ErrorMessage = "Qualification is Required")]
        [Range(1, 5)]
        public int Qualification { get; set; }
        [ForeignKey("Id")]
        public virtual ICollection<Gender> Genders { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
    }
}
