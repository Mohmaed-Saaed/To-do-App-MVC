using System.ComponentModel.DataAnnotations;

namespace To_do_App.ViewModels.AccountVM
{
    public class RegisterVM
    {

        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
