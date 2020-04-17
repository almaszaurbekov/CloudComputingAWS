using System.ComponentModel.DataAnnotations;
namespace UserInterface.Models
{
    public class UserViewModel
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage="Нельзя оставлять поле пустым")]
        public string Email { get; set; }

        [Range(0, 10000, ErrorMessage="Value for {0} must be between {1} and {2}.")]
        public double Wallet { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
