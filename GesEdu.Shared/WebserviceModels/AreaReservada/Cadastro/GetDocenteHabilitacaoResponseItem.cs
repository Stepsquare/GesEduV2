namespace GesEdu.Shared.WebserviceModels.AreaReservada.Cadastro
{
    public class GetDocenteHabilitacaoResponseItem
    {
        public int id_pessoa_unica { get; set; }
        public int id_habilitacao { get; set; }
        public int? id_anexo_pessoa { get; set; }
        public string? habilitacoes { get; set; }
        public string? inst_ensino { get; set; }
        public string? curso { get; set; }
        public string? class_acd { get; set; }
        public string? dt_conclusao { get; set; }
        public string? dt_efetivacao { get; set; }
        public string? desc_hab { get; set; }
        public string? fg_req { get; set; }
        public string? estado { get; set; }
    }
}
