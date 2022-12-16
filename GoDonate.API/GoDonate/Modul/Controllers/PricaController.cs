using GoDonate.Data;
using GoDonate.Helpers;
using GoDonate.Modul.Models;
using GoDonate.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoDonate.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PricaController : ControllerBase
    {
        private readonly GoDonateDbContext _dbContext;

        public PricaController(GoDonateDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public Prica Add([FromBody] PricaAddVM x)
        {
            var novaPrica = new Prica
            {
                Naslov = x.naslov,
                Opis = x.opis,
                NovcaniCilj = x.novcani_cilj,
                Lokacija = x.lokacija,
                kategorijaID = x.kategorija_id,
                korisnikID = x.korisnik_id,
                Slika = HelperSlike.ParsirajUbase(x.slika)

            };

            _dbContext.Add(novaPrica);
            _dbContext.SaveChanges();
            return novaPrica;

        }

        [HttpGet]
        public List<PricaAddVM> GetSvePrice()
        {
            var price = _dbContext.Price
                .OrderBy(s => s.Naslov)
                .Select(s => new PricaAddVM()
                {
                    id=s.Id,
                    naslov = s.Naslov,
                    opis = s.Opis,
                    novcani_cilj = s.NovcaniCilj,
                    lokacija = s.Lokacija,
                    kategorija_id = s.kategorijaID,
                    korisnik_id = s.korisnikID
                })
                .AsQueryable();
            return price.Take(100).ToList();
        }

        [HttpGet("{pricaid}")]
        public ActionResult GetSlikaPrice(int pricaid)
        {
            byte[] prica = _dbContext.Price.Find(pricaid).Slika;

            return File(prica, "image/png");
        }
    }
}
