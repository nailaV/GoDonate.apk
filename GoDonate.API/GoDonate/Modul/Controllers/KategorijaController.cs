using GoDonate.Data;
using GoDonate.Modul.Models;
using GoDonate.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoDonate.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KategorijaController : ControllerBase
    {
        private readonly GoDonateDbContext _dbContext;

        public KategorijaController(GoDonateDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public Kategorija Add([FromBody] KategorijaAddVM x)
        {
            var kategorija = new Kategorija
            {
                Naziv = x.naziv,
                Opis = x.opis
            };

            _dbContext.Add(kategorija);
            _dbContext.SaveChanges();
            return kategorija;

        }

        [HttpGet]
        public List<KategorijaAddVM> GetSveKategorije()
        {
            var kategorije = _dbContext.Kategorije
                .OrderBy(s => s.Naziv)
                .Select(s => new KategorijaAddVM()
                {
                    naziv=s.Naziv,
                    opis=s.Opis
                })
                .AsQueryable();
            return kategorije.Take(100).ToList();
        }
    }
}

