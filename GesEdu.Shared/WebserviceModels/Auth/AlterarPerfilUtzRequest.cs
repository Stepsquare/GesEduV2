using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.Auth
{
    public class AlterarPerfilUtzRequest
    {
        public int id_utilizador { get; set; }
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
