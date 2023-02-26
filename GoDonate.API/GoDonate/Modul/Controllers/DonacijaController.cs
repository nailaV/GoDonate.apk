using GoDonate.Data;
using GoDonate.Modul.Models;
using GoDonate.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoDonate.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DonacijaController:ControllerBase
    {
        private readonly GoDonateDbContext _dbContext;

        public DonacijaController(GoDonateDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public Donacija Add([FromBody] DonacijaAddVM x)
        {
            var novaDonacija = new Donacija
            {
                KolicinaNovca = x.kolicina_novca,
                Datum = x.datum,
                karticaID = x.kartica_id,
                pricaID=x.prica_id,
                korisnikID=x.korisnik_id
            };

            _dbContext.Add(novaDonacija);
            _dbContext.SaveChanges();
            return novaDonacija;

        }

        [HttpGet]
        public List<DonacijaAddVM> GetSveDonacije()
        {
            var donacije = _dbContext.Donacije
                .OrderBy(s => s.KolicinaNovca)
                .Select(s => new DonacijaAddVM()
                {
                    kolicina_novca = s.KolicinaNovca,
                    datum = s.Datum,
                    kartica_id = s.karticaID,
                    prica_id=s.pricaID,
                    korisnik_id=s.korisnikID
                })
                .AsQueryable();
            return donacije.Take(100).ToList();
        }

        [HttpGet("{pricaID}")]
        public ActionResult GetUkupnoZaPricu (int pricaID)
        {
            var ukupno = _dbContext.Donacije.Where(p => p.pricaID == pricaID).Sum(c => c.KolicinaNovca);
            return Ok(ukupno);
        }
    }
}
