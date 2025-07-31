namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetPedidosExcecaoResponseItem
    {
        public int id_pedido_cab { get; set; }
        public int id_ano_letivo { get; set; }
        public int id_excecao { get; set; }
        public string? des_excecao { get; set; }
        public int cod_uo { get; set; }
        public int? cod_escola_me { get; set; }
        public int? id_disciplina { get; set; }
        public string? des_disciplina { get; set; }
        public int? ano_escolar { get; set; }
        public string? des_ano_escolar { get; set; }
        public int id_estado { get; set; }
        public string? des_estado { get; set; }
        public string? dt_pedido { get; set; }
        public string? dt_estado_alt { get; set; }
    }
}
