using System.ComponentModel.DataAnnotations;

namespace Praesidium.ViewModels.Account
{
    public class RegisterInputModel
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required] 
        public string ConfirmPassword { get; set; }
        
        [Phone]
        public string PhoneNumber { get; set; }

        public string ReturnUrl { get; set; }
    }
}