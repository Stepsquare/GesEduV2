namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetEscolasCiclosResponse
    {
        public string? id_ano_letivo { get; set; }
        public string? des_ano_letivo { get; set; }
        public string? cod_uo { get; set; }
        public string? cod_escola_me { get; set; }
        public string? nome { get; set; }
        public List<Escola> escolas { get; set; } = [];

        public class Escola
        {
            public int cod_escola_me { get; set; }
            public string? nome_escola { get; set; }
            public List<Ciclo> ciclos { get; set; } = [];

            public class Ciclo
            {
                public string? cod_ciclo { get; set; }
                public string? des_ciclo { get; set; }
            }

        }
    }
}
