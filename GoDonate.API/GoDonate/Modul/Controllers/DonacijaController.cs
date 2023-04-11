using GoDonate.Data;
using GoDonate.Helpers;
using GoDonate.Modul.Models;
using GoDonate.Modul.SignalRHelper;
using GoDonate.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace GoDonate.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DonacijaController:ControllerBase
    {
        private readonly GoDonateDbContext _dbContext;
        private readonly IHubContext<NotifikacijeHub> notifikacijeHub;

        public DonacijaController(GoDonateDbContext dbContext, IHubContext<NotifikacijeHub> notifikacijeHub)
        {
            this._dbContext = dbContext;
            this.notifikacijeHub = notifikacijeHub;
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] DonacijaAddVM x)
        {
         

            var novaDonacija = new Donacija
            {
                KolicinaNovca = x.kolicina_novca,
                Datum = DateTime.Now,
                karticaID = x.kartica_id,
                pricaID=x.prica_id,
                korisnikID=x.korisnik_id
            };

            _dbContext.Donacije.Add(novaDonacija);
            await _dbContext.SaveChangesAsync();
            string poruka = $"New donation just happened. {x.kolicina_novca}$ is donated.";
            string korisnikovID = x.korisnik_id.ToString();
            await notifikacijeHub.Clients.AllExcept(new[] { korisnikovID }).SendAsync("PosaljiPoruke", poruka);
            return Ok();

        }

        [HttpGet]
        public List<DonacijaAddVM> GetSveDonacije()
        {
            var donacije = _dbContext.Donacije
                .OrderBy(s => s.KolicinaNovca)
                .Select(s => new DonacijaAddVM()
                {
                    kolicina_novca = s.KolicinaNovca,
                    kartica_id = s.karticaID,
                    prica_id=s.pricaID,
                    korisnik_id=s.korisnikID
                })
                .AsQueryable();
            return donacije.Take(100).ToList();
        }

    
 
        [HttpGet]
        public ActionResult GetUkupnoZaPrice()
        {
            //var ukupno = _dbContext.Donacije.Where(p => p.pricaID == pricaID).Sum(c => c.KolicinaNovca);

            //var prica = _dbContext.Price.FirstOrDefault(s => s.Id == pricaID);

            //if (ukupno >= prica.NovcaniCilj)
            //    prica.Aktivna = false;

            ////if (ukupno <= prica.NovcaniCilj)
            ////    prica.Aktivna = true;

            //_dbContext.SaveChanges();

            //return Ok(prica.Aktivna);
            using (var dbContext = _dbContext)
            {
                var stories = dbContext.Price.ToList();
                foreach (var story in stories)
                {
                    decimal totalDonations = dbContext.Donacije
                        .Where(d => d.pricaID == story.Id)
                        .Sum(d => d.KolicinaNovca);

                    if (totalDonations >= story.NovcaniCilj)
                    {
                        story.Aktivna = false;
                    }
                }

                _dbContext.SaveChanges();
            }
            return Ok();
        }

        [HttpGet("pricaID")]
        public ActionResult GetFormulu(int pricaID)
        {
            var data = _dbContext.Price.Where(p => p.Id == pricaID).Select(n => n.NovcaniCilj);



            return Ok(data);
        }

        [HttpGet("{pricaID}")]
        public ActionResult GetUkupnoZaFormulu(int pricaID)
        {
            var ukupno = _dbContext.Donacije.Where(p => p.pricaID == pricaID).Sum(c => c.KolicinaNovca);

            
            return Ok(ukupno);
        }

        [HttpGet]
        public List<object> GetTopDonators()
        {
            using (var dbContext = _dbContext)
            {
                var topDonators = dbContext.Donacije
                    .GroupBy(d => new { d.Korisnik.Ime, d.Korisnik.Prezime })
                    .Select(g => new
                    {
                        FullName = $"{g.Key.Ime} {g.Key.Prezime}",
                        TotalDonations = g.Sum(d => d.KolicinaNovca)
                    })
                    .OrderByDescending(d => d.TotalDonations)
                    .Take(3)
                    .ToList();

                return topDonators.Cast<object>().ToList();
            }
        }

    }
}
