using System.ComponentModel.DataAnnotations;

namespace BookingWizard.IdentityServer.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
