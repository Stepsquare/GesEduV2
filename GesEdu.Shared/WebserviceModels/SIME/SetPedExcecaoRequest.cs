namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class SetPedExcecaoRequest
    {
        public string? cod_uo { get; set; }
        public string? cod_escola_me { get; set; }
        public string? id_ano_letivo { get; set; }
        public int? id_disciplina { get; set; }
        public string? tipo_ensino { get; set; }
        public int? cod_agr_disc { get; set; }
        public int? id_excecao { get; set; }
        public string? utilizador { get; set; }
        public string? justif_pedido { get; set; }
    }
}
