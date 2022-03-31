using System.ComponentModel.DataAnnotations;

namespace AspNetMvcSocial.Data
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Kullanıcı adı alanı boş geçilemez")]
        [MinLength(3)]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [MaxLength(30)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAdress { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş geçilemez")]
        [MinLength(3)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre Tekrarı alanı boş geçilemez")]
        [MinLength(3)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifre ve tekrarı aynı olmalıdır!")]
        public string PasswordRepeat { get; set; }

        
    }
}
