using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GesEdu.Models.AuthenticationViewModels
{
    public class PasswordRecoveryViewModel
    {
        [EmailAddress(ErrorMessage = "* O email introduzido é inválido.")]
        [Required(ErrorMessage = "* Campo obrigatório.")]
        [DisplayName("E-Mail")]
        public string? Email { get; set; }
    }
}
