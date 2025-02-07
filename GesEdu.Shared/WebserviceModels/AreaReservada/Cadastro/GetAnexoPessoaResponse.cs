namespace GesEdu.Shared.WebserviceModels.AreaReservada.Cadastro
{
    public class GetAnexoPessoaResponse
    {
        public int id_anexo_pessoa { get; set; }
        public string? tipo_anexo { get; set; }
        public string? dt_criacao { get; set; }
        public string? anexo_base64 { get; set; }
        public string? filename { get; set; }
        public string? mimetype { get; set; }
    }
}
