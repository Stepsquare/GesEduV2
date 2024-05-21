using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        #region User info

        public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        public static string GetName(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst(ClaimTypes.Sid)?.Value ?? string.Empty;
        public static string GetCodigoOrigem(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("COD_ORIGEM")?.Value ?? string.Empty;
        public static string GetNomeOrigem(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("NOME_ORIGEM")?.Value ?? string.Empty;
        public static bool IsEscolaPrivada(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("COD_ORIGEM")?.Value == "ESC";
        public static string GetIdServico(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("ID_SERVICO")?.Value ?? string.Empty;
        public static string GetCodigoServico(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("COD_SERVICO")?.Value ?? string.Empty;
        public static string GetNomeServico(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("NOME_SERVICO")?.Value ?? string.Empty;
        public static string GetNifServico(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("NIF_SERVICO")?.Value ?? string.Empty;

        #endregion



        #region Ano letivo info

        public static string GetAnoLetivo(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("ANO_LETIVO")?.Value ?? string.Empty;
        public static string GetAnoLetivoDescription(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.FindFirst("DES_ANO_LETIVO")?.Value ?? string.Empty;
        public static string GetAnoLetivoAnterior(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.FindFirst("ANO_LETIVO_ANTERIOR")?.Value ?? string.Empty;
        public static string GetAnoLetivoAnteriorDescription(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.FindFirst("DES_ANO_LETIVO_ANTERIOR")?.Value ?? string.Empty;
        public static string GetFase(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.FindFirst("ESTADO_FASE")?.Value ?? string.Empty;

        #endregion



        #region GesEdu Permissions

        public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.IsInRole("ADMIN");
        public static bool IsUserManager(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.IsInRole("USER_MANAGER");
        public static bool IsMegaUser(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.IsInRole("MEGA_USER");
        public static bool IsAreaReservadaUser(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.IsInRole("AREA_RESERVADA_USER");
        public static bool IsAreaReservadaReactUser(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.IsInRole("AREA_RESERVADA_REACT_USER");
        public static bool IsSimeDgeUser(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.IsInRole("SIME_DGE_USER");
        public static bool IsSimeEscUser(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.IsInRole("SIME_ESC_USER");

        #endregion
    }
}
