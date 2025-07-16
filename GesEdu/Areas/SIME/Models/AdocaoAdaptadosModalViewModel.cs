namespace GesEdu.Areas.SIME.Models
{
    public class AdocaoAdaptadosModalViewModel
    {
        public bool IsReadOnly { get; set; } = false;
        public int IdAluno { get; set; }
        public string? NomeAluno { get; set; }
        public string? TipoDocAluno { get; set; }
        public string? NumDocAluno { get; set; }
        public int AnoEscolarAluno { get; set; }
        public string? TipoAdaptacao { get; set; }

        public List<Manual>? Manuais { get; set; }

        public class Manual
        {
            public int IdManual { get; set; }
            public int IdManualEscola { get; set; }
            public int IdDisciplina { get; set; }
            public string? DescDisciplina { get; set; }
            public string? Titulo { get; set; }
            public string? Isbn { get; set; }
        }
    }
}
