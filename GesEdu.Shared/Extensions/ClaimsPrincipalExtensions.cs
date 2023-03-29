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

        public static string? GetUsername(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        public static string? GetName(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
        public static string? GetUserId(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst(ClaimTypes.Sid)?.Value;
        public static string? GetCodigoOrigem(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("COD_ORIGEM")?.Value;
        public static string? GetNomeOrigem(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("NOME_ORIGEM")?.Value;
        public static bool IsEscolaPrivada(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("COD_ORIGEM")?.Value == "ESC";
        public static string? GetIdServico(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("ID_SERVICO")?.Value;
        public static string? GetCodigoServico(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("COD_SERVICO")?.Value;
        public static string? GetNomeServico(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("NOME_SERVICO")?.Value;
        public static string? GetNifServico(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("NIF_SERVICO")?.Value;

        #endregion



        #region Ano letivo info

        public static string? GetAnoLetivo(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst("ANO_LETIVO")?.Value;
        public static string? GetAnoLetivoDescription(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.FindFirst("DES_ANO_LETIVO")?.Value;
        public static string? GetAnoLetivoAnterior(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.FindFirst("ANO_LETIVO_ANTERIOR")?.Value;
        public static string? GetAnoLetivoAnteriorDescription(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.FindFirst("DES_ANO_LETIVO_ANTERIOR")?.Value;
        public static string? GetFase(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.FindFirst("ESTADO_FASE")?.Value;

        #endregion



        #region GesEdu Permissions

        public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.IsInRole("ADMIN");
        public static bool IsUserManager(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.IsInRole("USER_MEGA_READ");
        public static bool IsMegaReadUser(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.IsInRole("USER_MEGA_READ");
        public static bool IsMegaWriteUser(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.IsInRole("USER_MEGA_WRITE");
        public static bool IsAreaReservadaUser(this ClaimsPrincipal claimsPrincipal)
           => claimsPrincipal.IsInRole("USER_AREA_RSV");

        #endregion
    }
}
