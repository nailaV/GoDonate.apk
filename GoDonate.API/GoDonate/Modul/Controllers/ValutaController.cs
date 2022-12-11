using GoDonate.Data;
using GoDonate.Modul.Models;
using GoDonate.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoDonate.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ValutaController:ControllerBase
    {
        private readonly GoDonateDbContext _dbContext;

        public ValutaController(GoDonateDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public Valuta Add([FromBody] ValutaAddVM x)
        {
            var valuta = new Valuta
            {
                Naziv = x.naziv,
                Skracenica = x.skracenica
            };

            _dbContext.Add(valuta);
            _dbContext.SaveChanges();
            return valuta;

        }

        [HttpGet]
        public List<ValutaAddVM> GetSveValute()
        {
            var valute = _dbContext.Valute
                .OrderBy(s => s.Naziv)
                .Select(s => new ValutaAddVM()
                {
                    naziv = s.Naziv,
                    skracenica = s.Skracenica,
                    id=s.Id
                })
                .AsQueryable();
            return valute.Take(100).ToList();
        }
    }
}

