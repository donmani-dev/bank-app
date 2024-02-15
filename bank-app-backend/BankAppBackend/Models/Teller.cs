using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAppBackend.Models
{
    public class Teller
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Tellent email can't be empty")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Tellent name can't be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Teller password can't be empty")]
        public string Password { get; set; }
        [JsonIgnore]
        public ICollection<Applicant>? Applicants { get; set; }
    }
}
