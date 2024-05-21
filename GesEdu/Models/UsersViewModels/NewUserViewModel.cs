using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using GesEdu.Shared.WebserviceModels.Auth;

namespace GesEdu.Models.UsersViewModels
{
    public class NewUserViewModel
    {
        [Required(ErrorMessage = "* Campo obrigatório.")]
        [DisplayName("Nome")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "* Endereço de email inválido.")]
        [Required(ErrorMessage = "* Campo obrigatório.")]
        [DisplayName("Endereço de email")]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "* Campo obrigatório.")]
        [DisplayName("Password")]
        [MinLength(8, ErrorMessage = "A password tem de ter no mínimo 8 caracteres.")]
        [RegularExpression("^(?=.*[0-9]).+$", ErrorMessage = "A password tem de ter no mínimo 1 número.")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "* Campo obrigatório.")]
        [DisplayName("Confirmar Password")]
        [MinLength(6, ErrorMessage = "A password tem de ter no mínimo 6 caracteres.")]
        [Compare("Password", ErrorMessage = "As passwords são diferentes.")]
        public string? ConfirmNewPassword { get; set; }

        public List<CheckboxViewModel> Perfis { get; set; } = new List<CheckboxViewModel>();

        public NewUserViewModel()
        {

        }

        public NewUserViewModel(List<GetPerfisAppResponseItem> perfis)
        {
            Perfis = perfis.Select(x => new CheckboxViewModel
            {
                Id = x.ID_PERFIL,
                LabelName = x.DESCRICAO,
                IsChecked = false
            }).ToList();
        }
    }
}
