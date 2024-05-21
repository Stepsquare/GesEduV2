using GesEdu.Shared.WebserviceModels.Auth;

namespace GesEdu.Models.UsersViewModels
{
    public class UserPermissionViewModel
    {
        public UserPermissionViewModel()
        {

        }

        public UserPermissionViewModel(List<GetPerfisAppResponseItem> perfis, GetUtilizadorResponse userDetail)
        {
            UserId = userDetail.id_utilizador;
            Name = userDetail.nome;
            Email = userDetail.email;

            Perfis = perfis.Select(x => new CheckboxViewModel
            {
                Id = x.ID_PERFIL,
                LabelName = x.DESCRICAO,
                IsChecked = userDetail.perfis.Any(y => y.id_perfil == x.ID_PERFIL)
            }).ToList();
        }

        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public List<CheckboxViewModel> Perfis { get; set; } = new List<CheckboxViewModel>();
    }
}
