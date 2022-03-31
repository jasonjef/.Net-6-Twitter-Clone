using System.ComponentModel.DataAnnotations;

namespace AspNetMvcSocial.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? UserName { get; set; }
    }
}
