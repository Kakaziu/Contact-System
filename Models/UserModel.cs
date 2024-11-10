using System.ComponentModel.DataAnnotations;

namespace ContactSystem.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo 'Nome' precisa ser preenchido")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "O campo 'E-mail' precisa ser preenchido")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "O campo 'Senha' precisa ser preenchido")]
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; }

        public bool ValidPassword(string password)
        {
            return Password == password;
        }
    }
}
