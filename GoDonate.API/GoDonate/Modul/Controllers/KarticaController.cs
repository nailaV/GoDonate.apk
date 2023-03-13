using GoDonate.Data;
using GoDonate.Helpers;
using GoDonate.Modul.Models;
using GoDonate.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoDonate.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KarticaController:ControllerBase
    {
        private readonly GoDonateDbContext _dbContext;

        public KarticaController(GoDonateDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult AddKarticu([FromBody] KarticaAddVM x)
        {
            //int? id_logirani_korisnik = HttpContext.GetAuthToken()?.korisnickinalogID;
            //if (id_logirani_korisnik == null)
            //    return BadRequest();
            Kartica kartica;
            if (x.id == 0)
            {
                kartica = new Kartica();
                _dbContext.Add(kartica);
            }
            else
                kartica = _dbContext.Kartice.FirstOrDefault(s => s.Id == x.id);
            kartica.BrojKartice = x.brojKartice;
            kartica.TipKartice = x.tipKartice;
            kartica.CVV_CVC = x.cvv;
            kartica.MjesecIsteka = x.mjesecVazenja;
            kartica.GodinaIsteka = x.godinaVazenja;
            kartica.KorisnikID = x.korisnikID;

            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet("{korisnikID}")]
        public ActionResult GetKorisnikoveKartice(int korisnikID)
        {
            var data = _dbContext.Kartice.Where(p => p.KorisnikID== korisnikID).ToList().Select(s => new
            {
                id = s.Id,
                brojKartice = s.BrojKartice
            });
            return Ok(data);
        }

        [HttpGet("{korisnikID}")]
        public int CountKartica(int korisnikID)
        {
            var data = _dbContext.Kartice.Where(p => p.KorisnikID == korisnikID).Count();
            return data;
        }

    }
}
