using System.ComponentModel.DataAnnotations;


namespace KutuphaneSatis.DTOs.Request.UserRequest
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz (örnek@domain.com).")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string Password { get; set; }

    }
}
