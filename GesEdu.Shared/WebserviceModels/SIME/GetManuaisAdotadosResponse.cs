using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetManuaisAdotadosResponse
    {
        public GetManuaisAdotadosObject? manuais_adotados { get; set; }

        public class GetManuaisAdotadosObject
        {
            public string? ciclo { get; set; }
            public List<AnoEscolar> anos_escolares { get; set; } = [];

            public class AnoEscolar
            {
                public string? ano_escolar { get; set; }
                public List<Disciplina> disciplinas { get; set; } = [];

                public class Disciplina
                {
                    public int id_disciplina { get; set; }
                    public string? cod_disciplina { get; set; }
                    public string? des_disciplina { get; set; }
                    public List<Manual> manuais { get; set; } = [];

                    public class Manual
                    {
                        public int id_manual { get; set; }
                        public string? titulo { get; set; }
                        public string? ISBN { get; set; }
                        public string? editora { get; set; }
                        public decimal vlr_manual { get; set; }
                        public List<Escola> escolas { get; set; } = [];

                        public class Escola
                        {
                            public int id_escola { get; set; }
                            public int cod_escola_me { get; set; }
                            public string? nome_escola { get; set; }
                            public int estimativa { get; set; }
                            public int num_manuais_adaptados { get; set; }
                            public string? adotado_em { get; set; }
                        }
                    }
                }
            }
        }
    }
}
