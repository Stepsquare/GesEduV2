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
        public List<Perfil>? perfis { get; set; } = new List<Perfil>();

        public class Perfil
        {
            public int id_perfil { get; set; }

            /// <summary>
            /// Valor "A" para Ativar e "R" para Remover.
            /// </summary>
            public string? acao { get; set; }
        }
    }
}
