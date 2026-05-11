using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace To_do_App.ViewModels.AccountVM
{
    public class LoginVM
    {
        [Required]
        [DisplayName("User Name")]
        public  string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }
    }
}
