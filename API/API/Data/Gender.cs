using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class Gender
    {
        [Key]
        public int IdGender { get; set; }
        [Required(ErrorMessage = "The Gender Name is Required")]
        [StringLength(maximumLength: 250, ErrorMessage = "Gender Name Is Too Long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Image Name is Required")]
        [StringLength(maximumLength: 1000, ErrorMessage = "Gender Name Is Too Long")]
        public string Image { get; set; }
    }
}