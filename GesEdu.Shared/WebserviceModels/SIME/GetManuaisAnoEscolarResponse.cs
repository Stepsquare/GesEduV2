namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetManuaisAnoEscolarResponse
    {
        public int id_aluno { get; set; }
        public string? ano_escolar { get; set; }
        public string? cod_uo { get; set; }
        public List<Manual>? manuais { get; set; }

        public class Manual
        {
            public int id_manual { get; set; }
            public int id_manual_escola { get; set; }
            public string? titulo { get; set; }
            public int id_disciplina { get; set; }
            public string? cod_disciplina { get; set; }
            public string? des_disciplina { get; set; }
            public string? isbn { get; set; }
        }
    }
}
