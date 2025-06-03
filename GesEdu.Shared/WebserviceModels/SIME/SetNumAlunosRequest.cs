namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class SetNumAlunosRequest
    {
        public string? cod_uo { get; set; }
        public string? cod_escola_me { get; set; }
        public string? id_manual { get; set; }
        public string? id_ano_letivo { get; set; }
        public int num_alunos { get; set; }
        public int adquirir_manual { get; set; }
        public string? utilizador { get; set; }
    }
}
