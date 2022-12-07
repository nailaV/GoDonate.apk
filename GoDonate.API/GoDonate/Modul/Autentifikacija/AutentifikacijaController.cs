using GoDonate.Data;
using GoDonate.Helpers;
using GoDonate.Modul.Models;
using Microsoft.AspNetCore.Mvc;
using static GoDonate.Helpers.AuthTokenExtension;

namespace GoDonate.Modul.Autentifikacija
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AutentifikacijaController : ControllerBase
    {
        private readonly GoDonateDbContext _dbContext;

        public AutentifikacijaController(GoDonateDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpPost]
        public ActionResult<LoginInfo> Login ([FromBody] LoginVM x)
        {
            KorisnickiNalog korisnickiNalog = _dbContext.KorisnickiNalozi.
                FirstOrDefault(k=>k.Username != null && k.Username == x.username && k.Password == x.password);
            if(korisnickiNalog== null)
            {
                return new LoginInfo(null);
            }
            string randomString = TokenGenerator.Generate(10);
            var token = new AutentifikacijaToken
            {
                ipAdresa=Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                vrijednost=randomString,
                korisnickinalog=korisnickiNalog,
                vrijemeEvidencije=DateTime.Now
            };
            _dbContext.Add(token);
            _dbContext.SaveChanges();

            return new LoginInfo(token);
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();
            if (autentifikacijaToken == null)
                return Ok();
            _dbContext.Remove(autentifikacijaToken);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<AutentifikacijaToken> Get()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();

            return autentifikacijaToken;
        }
    }
}
