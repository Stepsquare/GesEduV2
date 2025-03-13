using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GesEdu.Shared.WebserviceModels.AreaReservada.Cadastro.GetFuncionariosUoRespResponse.Funcionario;

namespace GesEdu.Shared.WebserviceModels.Manuais
{
    public class GetUoResponseItem
    {
        public string Label { 
            get { return $"{Cod_agrupamento} - {Nome}"; } 
        }
        public int Id_servico { get; set; }
        public string? Cod_agrupamento { get; set; }
        public string? Nome { get; set; }
        public int Nif_servico { get; set; }
        public string? Diretor { get; set; }
        public List<Contacto> Contactos { get; set; } = new List<Contacto>();

        public class Contacto()
        {
            public string? Tipo_contacto { get; set; }
            public string? Valor_contacto { get; set; }
        }
    }
}
