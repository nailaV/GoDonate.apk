using GoDonate.Data;
using GoDonate.Helpers;
using GoDonate.Modul.Models;
using GoDonate.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            int? id_logirani_korisnik = HttpContext.GetAuthToken()?.korisnickinalogID;

            if (id_logirani_korisnik == null)
                return BadRequest();
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
            prica.korisnikID = id_logirani_korisnik.Value;
            if (x.slika != "")
            {
                byte[] slikaBajtovi = x.slika.ParsirajUbase();
                prica.Slika = slikaBajtovi;

            }
            
            prica.NovcaniCilj = x.novcani_cilj;
            prica.Lokacija = x.lokacija;
            prica.kategorijaID = x.kategorija_id;


            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet("{korisnikID}")]
        public ActionResult GetKorisnikovePrice(int korisnikID)
        {
            var data = _dbContext.Price.Where(p => p.korisnikID == korisnikID).ToList().Select(s => new
            {
                id=s.Id,
                naslov = s.Naslov,
                opis=s.Opis,
                novcani_cilj=s.NovcaniCilj
            });
            return Ok(data);
        }

        [HttpGet("{korisnikID}")]
        public ActionResult GetOstalePrice(int korisnikID)
        {
            var data = _dbContext.Price.Where(p => p.korisnikID != korisnikID).ToList().Select(s => new
            {
                id=s.Id,
                naslov = s.Naslov,
                opis=s.Opis,
                novcani_cilj=s.NovcaniCilj
            }).ToList();
            return Ok(data);
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

        [HttpGet]
        public ActionResult GetPricePaging(int pageNumber = 1, int pageSize = 5)
        {
            var price = _dbContext.Price.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList().Select(s => new
            {
                id = s.Id,
                naslov = s.Naslov,
                opis = s.Opis,
                novcani_cilj = s.NovcaniCilj
            });
            var totalItems = _dbContext.Price.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var odgovor = new
            {
                price,
                totalPages
            };
            return Ok(odgovor);
        }

        [HttpGet("{pricaid}")]
        public ActionResult GetSlikaPrice(int pricaid)
        {
            byte[]? prica = _dbContext.Price.Find(pricaid)?.Slika;

            if (prica == null)
                return BadRequest();

            return File(prica, "image/*");
        }

        [HttpGet("{pricaid}")]
        public ActionResult ObrisiPricu(int pricaid)
        {
            var prica = _dbContext.Price.FirstOrDefault(p=>p.Id== pricaid);
            _dbContext.Remove(prica);
            _dbContext.SaveChanges();
            return Ok();
        }



        [HttpGet("{id}")]
        public ActionResult GetByPricaId (int id)
        {
            var prica = _dbContext.Price.Where(x => x.Id == id)
                .Select(p => new 
                {
                    id=p.Id,
                    naslov = p.Naslov,
                    opis = p.Opis,
                    novcaniCilj = p.NovcaniCilj,
                    kategorija = p.Kategorija.Naziv,
                    lokacija = p.Lokacija
                }).AsQueryable();

            return Ok(prica.ToList());

        }
    }
}
