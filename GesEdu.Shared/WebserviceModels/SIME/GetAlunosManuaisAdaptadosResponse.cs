namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetAlunosManuaisAdaptadosResponse
    {
        public string? id_ano_letivo { get; set; }
        public string? cod_uo { get; set; }
        public List<AlunoManuaisAdaptados>? alunos_manuais_adaptados { get; set; }

        public class AlunoManuaisAdaptados
        {
            public int id_aluno { get; set; }
            public string? tipo_doc_aluno { get; set; }
            public string? num_doc_aluno { get; set; }
            public string? cd_doc_aluno { get; set; }
            public string? nome { get; set; }
            public int qtd_manuais { get; set; }
        }
    }
}
