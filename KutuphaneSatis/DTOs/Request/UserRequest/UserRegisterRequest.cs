using System.ComponentModel.DataAnnotations;

namespace KutuphaneSatis.DTOs.Request.UserRequest
{
    public class UserRegisterRequest
    {
        [Required(ErrorMessage = "Ad alanı boş bırakılamaz!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad alanı boş bırakılamaz!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz (örnek@domain.com).")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Şifre zorunludur.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Şifreniz 6 ile 20 karakter arasında olmalıdır.")]
        public string Password { get; set; }


    }
}
