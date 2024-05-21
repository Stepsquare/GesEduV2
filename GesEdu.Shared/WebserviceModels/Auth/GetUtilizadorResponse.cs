using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.Auth
{
    public class GetUtilizadorResponse
    {
        public int id_utilizador { get; set; }
        public string? responsavel { get; set; }
        public string? email { get; set; }
        public DateTime dt_inicial { get; set; }
        public string? nome { get; set; }
        public string? estado { get; set; }
        public List<Perfil> perfis { get; set; } = new List<Perfil>();

        public class Perfil
        {
            public int id_aplicacao { get; set; }
            public int id_perfil { get; set; }
            public string? cod_perfil { get; set; }
        }
    }
}
