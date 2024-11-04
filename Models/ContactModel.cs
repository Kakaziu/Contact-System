using System.ComponentModel.DataAnnotations;

namespace ContactSystem.Models
{
    public class ContactModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo 'Nome' tem que ser preenchido.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "O campo 'Email' tem que ser preenchido.")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "O campo 'Phone' tem que ser preenchido.")]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        public string? Phone { get; set; }
    }
}
