using System.ComponentModel.DataAnnotations;

namespace Entities.Dto
{
    public class DtoLogin
    {
        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        
        public string? Eposta { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        public string? Sifre { get; set; }
    }
}