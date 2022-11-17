using GoDonate.Data;
using GoDonate.Modul.Models;
using GoDonate.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        public List<GradAddVM> GetSviGradovi()
        {
            var gradovi = _dbContext.Gradovi
                .OrderBy(s => s.Naziv)
                .Select(s => new GradAddVM()
                {
                    naziv = s.Naziv,
                    postanskibroj = s.PostanskiBroj,
                    drzavaID = s.drzavaID
                })
                .AsQueryable();
            return gradovi.Take(100).ToList();
        }
    }
}
