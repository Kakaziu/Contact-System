using System.ComponentModel.DataAnnotations;

namespace ContactSystem.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O campo 'E-mail' precisa ser preenchido")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "O campo 'Senha' precisa ser preenchido")]
        public string? Password { get; set; }
    }
}
