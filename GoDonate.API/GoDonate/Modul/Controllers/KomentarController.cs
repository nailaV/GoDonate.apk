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
                    Id = s.Id,
                    napisaoIme = s.Korisnik.Ime,
                    napisaoPrezime = s.Korisnik.Prezime,
                    sadrzajKomentara = s.Sadrzaj,
                    brojLajkova = s.brojLajkova,
                    brojDislajkova = s.brojDislajkova,
                    korisnikID = s.korisnikID
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

        [HttpPost("{komentarID}")]
        public ActionResult Obrisi(int komentarID)
        {
            var data = _dbContext.Komentari.Find(komentarID);

            _dbContext.Remove(data);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet("{komentarID}")]
        public ActionResult<int> GetBrojLajkova(int komentarID)
        {
            var data = _dbContext.Komentari.FirstOrDefault(k => k.Id == komentarID);

            return data.brojLajkova;
        }

        [HttpPost("{komentarID}")]
        public ActionResult Like(int komentarID)
        {
            var prica = _dbContext.Komentari.FirstOrDefault(k => k.Id == komentarID);
            prica.brojLajkova++;
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpGet("{komentarID}")]
        public ActionResult<int> GetBrojDislajkova(int komentarID)
        {
            var data = _dbContext.Komentari.FirstOrDefault(k => k.Id == komentarID);

            return data.brojDislajkova;
        }

        [HttpPost("{komentarID}")]
        public ActionResult Dislike(int komentarID)
        {
            var prica = _dbContext.Komentari.FirstOrDefault(k => k.Id == komentarID);
            prica.brojDislajkova++;
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
