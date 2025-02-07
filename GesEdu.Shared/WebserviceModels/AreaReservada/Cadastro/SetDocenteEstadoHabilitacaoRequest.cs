namespace GesEdu.Shared.WebserviceModels.AreaReservada.Cadastro
{
    public class SetDocenteEstadoHabilitacaoRequest
    {
        public int id_habilitacao { get; set; }
        public string? estado_habilitacao { get; set; }
        public string? fg_req { get; set; }
        public string? dt_efetivacao { get; set; }
    }
}
