using GoDonate.Data;
using GoDonate.Modul.Models;
using GoDonate.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public ActionResult GetSveKorisnike()
        {
            var korisnici = _dbContext.Korisnici
                .OrderBy(s => s.Ime)
                .Select(s => new KorisnikGetAllVM()
                {
                    id=s.ID,
                    ime = s.Ime,
                    prezime = s.Prezime,
                    datum_rodj = s.DatumRodjenja,
                    opstina_rodjenja_id = s.gradID,
                    opstina_rodjenja_opis=s.Grad.Naziv,
                    drzava_rodjenja_opis=s.Grad.Drzava.NazivDrzave
                })
                .ToList();
            return Ok(korisnici);
        }

    }
}
