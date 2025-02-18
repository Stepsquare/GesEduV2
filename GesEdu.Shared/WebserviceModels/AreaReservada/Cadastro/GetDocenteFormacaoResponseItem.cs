namespace GesEdu.Shared.WebserviceModels.AreaReservada.Cadastro
{
    public class GetDocenteFormacaoResponseItem
    {
        public int id_pessoa_unica { get; set; }
        public int id_formacao { get; set; }
        public int? id_anexo_pessoa { get; set; }
        public string? nif { get; set; }
        public string? nome { get; set; }
        public string? formacao { get; set; }
        public string? instituicao { get; set; }
        public string? classificacao { get; set; }
        public string? dt_inicio { get; set; }
        public string? dt_fim { get; set; }
        public string? num_horas { get; set; }
        public string? fg_art_8 { get; set; }
        public string? fg_art_9 { get; set; }
        public string? estado { get; set; }
    }
}
