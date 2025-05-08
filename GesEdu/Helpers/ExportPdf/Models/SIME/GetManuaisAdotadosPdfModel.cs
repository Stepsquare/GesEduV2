using GesEdu.Shared.WebserviceModels.SIME;

namespace GesEdu.Helpers.ExportPdf.Models.SIME
{
    public class GetManuaisAdotadosPdfModel
    {
        public string? AnoLetivo { get; set; }
        public GetManuaisAdotadosListagemResponse? EnsinoRegular { get; set; }
        public GetManuaisAdotadosListagemResponse? EnsinoProfessional { get; set; }
        public GetManuaisAdotadosListagemResponse? CursosEducacaoFormacao { get; set; }
    }
}
