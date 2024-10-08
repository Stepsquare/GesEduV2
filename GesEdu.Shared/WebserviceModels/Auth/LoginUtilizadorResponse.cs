using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.Auth
{
    public class LoginUtilizadorResponse : GenericPostResponse
    {
        public string? id_utilizador { get; set; }
        public string? nome { get; set; }
        public string? nif_servico { get; set; }
        public string? cod_origem { get; set; }
        public string? nome_origem { get; set; }
        public string? id_servico { get; set; }
        public string? cod_servico { get; set; }
        public string? nome_servico { get; set; }
        public string? url_apex { get; set; }
        public string? responsavel { get; set; }
        public string? trocar_password { get; set; }
        public List<Perfil> perfis { get; set; } = new List<Perfil>();

        public class Perfil
        {
            public string? cod_perfil { get; set; }
            public List<Objecto> objetos { get; set; } = new List<Objecto>();

            public class Objecto
            {
                public string? aplicacao { get; set; }
                public string? codigo { get; set; }
                public string? descricao { get; set; }
                public string? escrever { get; set; }
                public string? ler { get; set; }
                public string? aprovar { get; set; }
            }
        }
    }
}
