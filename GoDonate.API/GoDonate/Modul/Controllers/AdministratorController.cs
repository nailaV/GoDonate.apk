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
            return Ok(lista);
        }
    }
}
