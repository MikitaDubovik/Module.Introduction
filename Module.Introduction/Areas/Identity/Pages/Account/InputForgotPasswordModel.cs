using System.ComponentModel.DataAnnotations;

namespace Module.Introduction.Areas.Identity.Pages.Account
{
    public class InputForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
