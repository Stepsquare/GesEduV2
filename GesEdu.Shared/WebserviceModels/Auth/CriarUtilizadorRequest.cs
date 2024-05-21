using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.Auth
{
    public class CriarUtilizadorRequest
    {
        public string? id_utilizador { get; set; }
        public string? email { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? nome { get; set; }
        public string? dt_inicial { get; set; }
        public string? servico { get; set; }
        public string? responsavel { get; set; }
        public string? descricao { get; set; }
        public List<perfil>? perfis { get; set; } = new List<perfil>();

        public class perfil
        {
            public int id_perfil { get; set; }

            /// <summary>
            /// Valor "A" para Ativar e "R" para Remover.
            /// </summary>
            public string? acao { get; set; }
        }
    }
}
