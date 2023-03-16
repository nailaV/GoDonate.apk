using GoDonate.Data;
using GoDonate.Modul.Models;
using GoDonate.Modul.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoDonate.Modul.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class KomentarController : ControllerBase
    {
        private readonly GoDonateDbContext _dbContext;
        public KomentarController(GoDonateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult AddKomentar([FromBody] KomentarADDVM x)
        {
            Komentar komentar = new Komentar()
            {
                Sadrzaj = x.sadrzaj,
                pricaID= x.pricaID,
                korisnikID=x.korisnikID
            };
            _dbContext.Add(komentar);
            _dbContext.SaveChanges();
            return Ok(komentar);
        }
        [HttpGet("pricaID")]
        public ActionResult GetKomentareZaPricu(int pricaID)
        {
            var data = _dbContext.Komentari.Where(k => k.pricaID == pricaID).Select(s => new
            {
                Napisao = s.Korisnik.Ime + " "+ s.Korisnik.Prezime,
                Sadrzaj = s.Sadrzaj
            });
            return Ok(data);
        }

        [HttpGet("pricaID")]
        public int GetBrojKomentara(int pricaID)
        {
            var data = _dbContext.Komentari.Where(p => p.pricaID == pricaID).Count();
            return data;
        }

    }
}
