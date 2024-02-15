using System.ComponentModel.DataAnnotations;

namespace BankAppBackend.Models
{
    public enum UserType
    {
        TELLER,
        CUSTOMER
    }
    public class User
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email can't be empty")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password can't be empty")]
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
