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
        public ActionResult Add([FromBody] PricaAddVM x)
        {
            Prica prica;
            if (x.id == 0)
            {
                prica = new Prica();
                _dbContext.Add(prica);
            }
            else
                prica = _dbContext.Price.FirstOrDefault(s => s.Id == x.id);
            prica.Naslov = x.naslov;
            prica.Opis = x.opis;
            if (x.slika != "")
            {
                byte[] slikaBajtovi = x.slika.ParsirajUbase();
                prica.Slika = slikaBajtovi;

            }
            
            prica.NovcaniCilj = x.novcani_cilj;
            prica.Lokacija = x.lokacija;
            prica.kategorijaID = x.kategorija_id;
            prica.korisnikID = x.korisnik_id;

            _dbContext.SaveChanges();
            return Ok();


        }

        [HttpGet]
        public List<PricaAddVM> GetSvePrice()
        {
            var price = _dbContext.Price
                .OrderBy(s => s.Naslov)
                .Select(s => new PricaAddVM()
                {
                    id = s.Id,
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

            return File(prica, "image/*");
        }

    }
}
