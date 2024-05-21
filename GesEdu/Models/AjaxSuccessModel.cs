using static GesEdu.Shared.WebserviceModels.GenericPostResponse;

namespace GesEdu.Models
{
    public class AjaxSuccessModel
    {
        public bool IsMessage { get; set; } = false;
        public bool IsRedirect { get; set; } = false;
        public string? Message { get; set; }
        public string? Url { get; set; }

        internal AjaxSuccessModel AddMessage(string? message)
        {
            IsMessage = true;
            Message = message;

            return this;
        }

        internal AjaxSuccessModel AddRedirectUrl(string? url)
        {
            IsRedirect = true;
            Url = url;

            return this;
        }
    }
}
