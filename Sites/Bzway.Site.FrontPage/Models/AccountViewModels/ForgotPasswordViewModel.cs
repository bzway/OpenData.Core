using System.ComponentModel.DataAnnotations;

namespace Bzway.Site.FrontPage.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
