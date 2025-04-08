using System.ComponentModel.DataAnnotations;

namespace BCommerce.Web.Models
{
    public class LoginModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
