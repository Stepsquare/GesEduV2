using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GesEdu.Models.AuthenticationViewModels
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "* O nome de utilizador não é um Email válido.")]
        [Required(ErrorMessage = "* Campo obrigatório.")]
        [DisplayName("Utilizador")]
        public string? Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "* Campo obrigatório.")]
        [DisplayName("Password")]
        [MinLength(6, ErrorMessage = "A password tem de ter no mínimo 6 caracteres.")]
        public string? Password { get; set; }
    }
}
