namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetManuaisSIMEResponse
    {
        public string? ano_letivo { get; set; }
        public string? ciclo { get; set; }

        public List<Editora>? editores { get; set; }

        public class Editora
        {
            public string? nome_editora { get; set; }

            public List<Manual>? manuais { get; set; }

            public class Manual
            {
                public int id_manual { get; set; }
                public string? des_ano_escolar { get; set; }
                public string? des_disciplina { get; set; }
                public string? titulo { get; set; }
                public string? isbn { get; set; }
                public string? autores { get; set; }
                public decimal preco { get; set; }
                public bool cod_certificacao { get; set; }
                public string? des_certificacao { get; set; }
            }
        }
    }
}
