namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class SetAlunosManuaisAdaptadosRequest
    {
        public string? id_ano_letivo { get; set; }
        public int id_aluno { get; set; }
        public string? utilizador { get; set; }
        public List<ManualAdaptado>? manuais_adaptados { get; set; }

        public class ManualAdaptado
        {
            public int id_manual_escola { get; set; }
            public string? tp_adaptacao { get; set; }
        }
    }
}
