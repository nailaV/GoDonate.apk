using GoDonate.Data;
using GoDonate.Helpers;
using GoDonate.Modul.Models;
using GoDonate.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoDonate.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdministratorController : ControllerBase
    {
        private readonly GoDonateDbContext _dbContext;
        public AdministratorController(GoDonateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult DodajAdmina([FromBody] AdminVM x)
        {
            Administrator admin = new Administrator();
            admin.Ime = x.ime;
            admin.Prezime = x.prezime;
            admin.Username = x.username;
            admin.Password=x.password;
            admin.SlikaKorisnika = x.slikaKorisnika.ParsirajUbase();
            admin.Email = x.email;
            admin.Password=x.password;
            admin.BrojTelefona = x.brojtel;

            _dbContext.Add(admin);
            _dbContext.SaveChanges();


            return Ok();
        }

        

        [HttpGet]
        public ActionResult GetSve()
        {
            var lista = _dbContext.Administratori.ToList();
            EmailHelper.Posalji("naila.vejo4@gmail.com", "smijesno", "smijesno");
            return Ok(lista);
        }

        [HttpPost("{korisnikID}")]
        public ActionResult ObrisiKorisnika(int korisnikID)
        {
            
            var data = _dbContext.Korisnici.FirstOrDefault(k => k.ID == korisnikID);
            _dbContext.Remove(data);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public ActionResult PromjeniSliku([FromBody] PromjenaSlikeVM x)
        {
            var admin = _dbContext.Administratori.Find(x.id);
            if (admin == null)
                return BadRequest();
            else
            {
                admin.SlikaKorisnika = x.slikaKorisnika.ParsirajUbase();
                _dbContext.SaveChanges();
            }
            return Ok();
        }
    }
}
