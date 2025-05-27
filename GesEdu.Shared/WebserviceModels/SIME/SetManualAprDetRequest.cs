namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class SetManualAprDetRequest
    {
        public string? cod_uo { get; set; }
        public string? cod_escola_me { get; set; }
        public string? id_ano_letivo { get; set; }
        public string? id_manual { get; set; }
        public string? utilizador { get; set; }
        public List<ApreciacaoObj> apreciacoes { get; set; } = new List<ApreciacaoObj>();

        public class ApreciacaoObj
        {
            public string? id_valor_dominio { get; set; }
            public string? avaliacao { get; set; }
            public List<ApreciacaoObj> subalineas { get; set; } = new List<ApreciacaoObj>();
        }
    }
}
