using System.ComponentModel.DataAnnotations;


namespace TPS.ViewModels
{
    public class AccountSignInViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name = "Remeber me?")]
        public bool RememberMe { get; set; }
    }
}