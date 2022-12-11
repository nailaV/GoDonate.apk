using GoDonate.Data;
using GoDonate.Modul.Models;
using GoDonate.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoDonate.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GradController:ControllerBase
    {

        private readonly GoDonateDbContext _dbContext;

        public GradController(GoDonateDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public Grad Add([FromBody] GradAddVM x)
        {
            var noviGrad = new Grad
            {
                Naziv = x.naziv,
                PostanskiBroj = x.postanskibroj,
                drzavaID = x.drzavaID
            };

            _dbContext.Add(noviGrad);
            _dbContext.SaveChanges();
            return noviGrad;

        }

        [HttpGet]
        public ActionResult GetSviGradovi()
        {
            var gradovi = _dbContext.Gradovi
                .OrderBy(s => s.Naziv)
                .Select(s => new GradoviVM()
                {
                    id = s.GradID,
                    naziv = s.Naziv,
                    postanskiBroj = s.PostanskiBroj,
                    drzava = s.Drzava.NazivDrzave
                })
                .ToList();
            return Ok(gradovi);
        }
    }
}
