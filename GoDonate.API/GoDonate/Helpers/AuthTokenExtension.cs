using GoDonate.Data;
using GoDonate.Modul.Autentifikacija;
using GoDonate.Modul.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace GoDonate.Helpers
{
    public static class AuthTokenExtension
    {
        public class LoginInfo
        {
            public LoginInfo(AutentifikacijaToken autentifikacijaToken)
            {
                this.autentifikacijaToken = autentifikacijaToken;
            }
            [JsonIgnore]
            public KorisnickiNalog korisnickiNalog => autentifikacijaToken?.korisnickinalog;
            public AutentifikacijaToken autentifikacijaToken { get; set; }
            public bool isLogiran => korisnickiNalog != null;
        }

        public static LoginInfo GetLoginInfo(this HttpContext httpContext)
        {
            var token = httpContext.GetAuthToken();

            return new LoginInfo(token);
        }

        public static AutentifikacijaToken GetAuthToken(this HttpContext httpContext) {
            string token = httpContext.GetMyAuthToken();
            GoDonateDbContext db = httpContext.RequestServices.GetService<GoDonateDbContext>();
            AutentifikacijaToken korisnickiNalog = db.AutentifikacijaToken.Include(s => s.korisnickinalog).SingleOrDefault(x=> token!=null && x.vrijednost==token);
            return korisnickiNalog;
        }

        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["autentifikacija-token"];
            return token;
        }
    }


}
