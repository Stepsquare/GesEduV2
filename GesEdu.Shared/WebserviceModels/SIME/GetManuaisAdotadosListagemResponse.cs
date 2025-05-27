namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetManuaisAdotadosListagemResponse
    {
        public string? id_ano_letivo { get; set; }
        public string? des_ano_letivo { get; set; }
        public string? cod_agrupamento { get; set; }
        public string? nome_agrupamento { get; set; }
        public string? MSG_BUTTON { get; set; }
        public List<Escola> escolas { get; set; } = [];

        public class Escola
        {
            public int cod_escola_me { get; set; }
            public string? nome_escola { get; set; }
            public string? morada_escola { get; set; }
            public string? cp4_escola { get; set; }
            public string? cp3_escola { get; set; }
            public string? localidade_escola { get; set; }
            public List<AnoEscolar> ano_escolar { get; set; } = [];

            public class AnoEscolar
            {
                public string? ano_escolar { get; set; }
                public string? des_ano_escolar { get; set; }
                public List<Manual> manuais { get; set; } = [];

                public class Manual
                {
                    public int id_disciplina { get; set; }
                    public string? des_disciplina { get; set; }
                    public string? tipologia { get; set; }
                    public string? titulo { get; set; }
                    public string? isbn { get; set; }
                    public string? autor { get; set; }
                    public string? editora { get; set; }
                    public decimal preco { get; set; }
                    public int num_alunos { get; set; }
                }
            }
        }
    }
}
