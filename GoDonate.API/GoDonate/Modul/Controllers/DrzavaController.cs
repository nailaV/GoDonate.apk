using GoDonate.Data;
using GoDonate.Helpers;
using GoDonate.Modul.Models;
using GoDonate.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoDonate.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    
    
    public class DrzavaController:ControllerBase
    {
        private readonly GoDonateDbContext _dbContext;

        public DrzavaController(GoDonateDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public Drzava Add([FromBody] DrzavaAddVM x)
        {
            var novaDrzava = new Drzava
            {
                NazivDrzave = x.naziv,
                Skracenica = x.skracenica,
                valutaID = x.valutaID,
            };

            _dbContext.Add(novaDrzava);
            _dbContext.SaveChanges();
            return novaDrzava;

        }

        [HttpGet]
        public List<DrzavaAddVM> GetSveDrzave()
        {
            var drzave = _dbContext.Drzave
                .OrderBy(s => s.NazivDrzave)
                .Select(s => new DrzavaAddVM()
                {
                    naziv = s.NazivDrzave,
                    skracenica = s.Skracenica,
                    valutaID=s.valutaID
                })
                .AsQueryable();
            return drzave.Take(100).ToList();
        }
    }
}

