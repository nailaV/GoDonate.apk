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
    public class KorisnikController:ControllerBase
    {
        private readonly GoDonateDbContext _dbContext;

        public KorisnikController(GoDonateDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public ActionResult Add([FromBody] KorisnikAddVM x)
        {
            Korisnik korisnik;
            if (x.id == 0)
            {
                korisnik = new Korisnik();
                _dbContext.Add(korisnik);
            }
            else
                korisnik = _dbContext.Korisnici.FirstOrDefault(s => s.ID == x.id);

            korisnik.Ime = x.ime;
            korisnik.Prezime = x.prezime;
            korisnik.DatumRodjenja = x.datum_rodjenja;
            korisnik.gradID = x.grad_id;
            korisnik.valutaID = x.valuta_id;
            korisnik.Username= x.username;
            korisnik.Password = x.password;
            korisnik.Email = x.email;
            korisnik.BrojTelefona = x.brojTelefona;

            if(x.slikaKorisnika!="")
            {
                byte[] slikaBajtovi = x.slikaKorisnika.ParsirajUbase();
                korisnik.SlikaKorisnika = slikaBajtovi;
            }

            _dbContext.SaveChanges();
            return Ok();

        }

        [HttpGet("{korisnikid}")]
        public ActionResult GetSlikuKorisnika(int korisnikid)
        {
            byte[] korisnik = _dbContext.KorisnickiNalozi.Find(korisnikid).SlikaKorisnika;

            return File(korisnik, "image/*");
        }

        [HttpGet]
        public ActionResult GetSveKorisnike()
        {
            var korisnici = _dbContext.Korisnici
                .OrderBy(s =>s.ID)
                .Select(s => new KorisnikGetAllVM()
                {
                    id=s.ID,
                    ime = s.Ime,
                    prezime = s.Prezime,
                    datum_rodj = s.DatumRodjenja,
                    opstina_rodjenja_id = s.gradID,
                    opstina_rodjenja_opis=s.Grad.Naziv,
                    drzava_rodjenja_opis=s.Grad.Drzava.NazivDrzave,
                    email=s.Email,
                    brojTelefona=s.BrojTelefona,
                    username=s.Username
                })
                .ToList();
            return Ok(korisnici);
        }

        [HttpPost]
        public ActionResult PromjeniSliku([FromBody] PromjenaSlikeVM x)
        {
            var korisnik = _dbContext.Korisnici.Find(x.id);
            if (korisnik == null)
                return BadRequest();
            else
            {
                korisnik.SlikaKorisnika = x.slikaKorisnika.ParsirajUbase();
                _dbContext.SaveChanges();
            }
            return Ok();
        }


        [HttpPost]
        public ActionResult PromjeniPassword([FromBody] PromjenaPasswordaVM x)
        {
            var korisnik = _dbContext.Korisnici.Find(x.id);
            if (korisnik == null)
                return BadRequest();
            else if (korisnik.Password != x.stariPassword)
                return BadRequest();
            else
            {
                korisnik.Password = x.noviPassword;
                _dbContext.SaveChanges();
            }
            
            return Ok();
        }


    }
}
