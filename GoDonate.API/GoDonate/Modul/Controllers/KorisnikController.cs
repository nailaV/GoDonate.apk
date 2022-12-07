using GoDonate.Data;
using GoDonate.Modul.Models;
using GoDonate.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoDonate.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KorisnikController:ControllerBase
    {
        private readonly GoDonateDbContext _dbContext;

        public KorisnikController(GoDonateDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public Korisnik Add([FromBody] KorisnikAddVM x)
        {
            var noviKorisnik = new Korisnik
            {
                Ime = x.ime,
                Prezime = x.prezime,
                DatumRodjenja = x.datum_rodjenja,
                gradID = x.grad_id,
                valutaID = x.valuta_id,
                Username= x.username,
                Password=x.password
                
            };

            _dbContext.Add(noviKorisnik);
            _dbContext.SaveChanges();
            return noviKorisnik;

        }

        [HttpGet]
        public List<KorisnikAddVM> GetSveKorisnike()
        {
            var korisnici = _dbContext.Korisnici
                .OrderBy(s => s.Ime)
                .Select(s => new KorisnikAddVM()
                {
                    ime = s.Ime,
                    prezime = s.Prezime,
                    datum_rodjenja = s.DatumRodjenja,
                    grad_id = s.gradID,
                    valuta_id = s.valutaID
                    
                })
                .AsQueryable();
            return korisnici.Take(100).ToList();
        }

    }
}
