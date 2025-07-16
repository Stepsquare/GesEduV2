namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetDetAlunosManAdaptadosResponse
    {
        public string? id_ano_letivo { get; set; }
        public int id_aluno { get; set; }
        public List<ManualAdaptado>? manuais_adaptados { get; set; }

        public class ManualAdaptado
        {
            public int id_manual { get; set; }
            public string? titulo { get; set; }
            public int id_disciplina { get; set; }
            public string? cod_disciplina { get; set; }
            public string? des_disciplina { get; set; }
            public string? isbn { get; set; }
            public string? tp_adaptacao { get; set; }
        }
    }
}
