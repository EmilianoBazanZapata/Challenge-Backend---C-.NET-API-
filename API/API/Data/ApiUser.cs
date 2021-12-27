using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class ApiUser : IdentityUser
    {
        public string FisrtName { get; set; }
        public string LastName { get; set; }
    }
}
