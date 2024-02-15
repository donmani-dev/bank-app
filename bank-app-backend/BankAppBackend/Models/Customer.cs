using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankAppBackend.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerId {  get; set; }
        
        [Required(ErrorMessage = "User name can't be empty")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "User password can't be empty")]
        public string Password { get; set; }
        public long ApplicantId { get; set; }
        public Applicant? Applicant { get; set; }
        [JsonIgnore]
        public ICollection<Account>? Accounts { get; set; }

    }
}
