using GoDonate.Data;
using GoDonate.Migrations;
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
        [HttpGet]
        public ActionResult GetKomentareZaPricu(int pricaID, int pageNumber = 1, int pageSize = 5)
        {
            var komentari = _dbContext.Komentari.OrderByDescending(p => p.Id).
                Skip((pageNumber - 1) * pageSize).Take(pageSize).Where(k => k.pricaID == pricaID).Select(s => new
                {
                    napisaoIme = s.Korisnik.Ime,
                    napisaoPrezime = s.Korisnik.Prezime,
                    sadrzajKomentara = s.Sadrzaj
                });
            var totalItems = _dbContext.Komentari.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var odgovor = new
            {
                komentari,
                totalPages
            };
            return Ok(odgovor);
        }

        [HttpGet("{pricaID}")]
        public int GetBrojKomentara(int pricaID)
        {
            var data = _dbContext.Komentari.Where(p => p.pricaID == pricaID).Count();
            return data;
        }

    }
}
