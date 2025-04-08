using System.ComponentModel.DataAnnotations;

namespace BCommerce.Web.Models
{
    public class RegisterModel
    {
        [Required]
        public string NameSurname { get; set; }


        [EmailAddress]
        public string Email { get; set; }

        [Required]

        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string PasswordAgain { get; set; }
    }
}
