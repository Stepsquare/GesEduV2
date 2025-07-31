namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetDisciplinasPLNMResponse
    {
        public string? tipo_ensino { get; set; }
        public string? des_tipo_ensino { get; set; }
        public List<Disciplina>? disciplinas { get; set; }

        public class Disciplina
        {
            public int cod_agr_disc { get; set; }
            public string? des_disciplina { get; set; }
        }
    }
}
