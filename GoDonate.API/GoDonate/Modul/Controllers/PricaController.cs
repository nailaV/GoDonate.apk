﻿using GoDonate.Data;
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
    public class PricaController : ControllerBase
    {
        private readonly GoDonateDbContext _dbContext;
        private readonly IHubContext<NotifikacijeHub> notifikacijeHub;

        public PricaController(GoDonateDbContext dbContext, IHubContext<NotifikacijeHub> notifikacijeHub)
        {
            this._dbContext = dbContext;
            this.notifikacijeHub = notifikacijeHub;
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] PricaAddVM x)
        {

            if (HttpContext.GetLoginInfo().korisnickiNalog.isAdmin)
                return BadRequest("Admins can not add stories");

            int? id_logirani_korisnik = HttpContext.GetAuthToken()?.korisnickinalogID;


            if (id_logirani_korisnik == null)
                return BadRequest();
            Prica prica;
            if (x.id == 0)
            {
                prica = new Prica();
                _dbContext.Add(prica);
            }
            else
                prica = _dbContext.Price.FirstOrDefault(s => s.Id == x.id);
            prica.Naslov = x.naslov;
            prica.Opis = x.opis;
            prica.korisnikID = id_logirani_korisnik.Value;
            if (x.slika != "")
            {
                byte[] slikaBajtovi = x.slika.ParsirajUbase();
                prica.Slika = slikaBajtovi;

            }
            
            prica.NovcaniCilj = x.novcani_cilj;
            prica.Lokacija = x.lokacija;
            prica.kategorijaID = x.kategorija_id;

            await _dbContext.SaveChangesAsync();
            string poruka = $"New story has been added. {x.naslov} is available for donations - the goal is {x.novcani_cilj}$.";
            string korisnikovID = x.korisnik_id.ToString();
            await notifikacijeHub.Clients.AllExcept(new[] { korisnikovID }).SendAsync("PosaljiPoruke", poruka);
            return Ok();
        }

        [HttpGet("{korisnikID}")]
        public ActionResult GetKorisnikovePrice(int korisnikID)
        {
            var data = _dbContext.Price.Where(p => p.korisnikID == korisnikID).ToList().Select(s => new
            {
                id=s.Id,
                naslov = s.Naslov,
                opis=s.Opis,
                novcani_cilj=s.NovcaniCilj
            });
            return Ok(data);
        }

        [HttpGet("{korisnikID}")]
        public ActionResult GetOstalePrice(int korisnikID)
        {
            var data = _dbContext.Price.Where(p => p.korisnikID != korisnikID).ToList().Select(s => new
            {
                id=s.Id,
                naslov = s.Naslov,
                opis=s.Opis,
                novcani_cilj=s.NovcaniCilj
            }).ToList();
            return Ok(data);
        }

        [HttpGet("{pricaID}")]
        public ActionResult GetMoneyGoal(int pricaID)
        {
            var data = _dbContext.Price.FirstOrDefault(p => p.Id == pricaID);

            return Ok(data.NovcaniCilj);
        }


        [HttpGet]
        public List<PricaAddVM> GetSvePrice()
        {
            var price = _dbContext.Price
                .OrderBy(s => s.Naslov)
                .Select(s => new PricaAddVM()
                {
                    id = s.Id,
                    naslov = s.Naslov,
                    opis = s.Opis,
                    novcani_cilj = s.NovcaniCilj,
                    lokacija = s.Lokacija,
                    kategorija_id = s.kategorijaID,
                    korisnik_id = s.korisnikID
                })
                .AsQueryable();
            return price.Take(100).ToList();
        }

        [HttpGet]
        public ActionResult GetOtherStoriesPaging(int korisnikID, int pageNumber = 1, int pageSize = 5)
        {
            var price = _dbContext.Price.Where(k => k.korisnikID != korisnikID && k.Aktivna != false).OrderByDescending(p=>p.Id).
                Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList().Select(s => new
            {
                id = s.Id,
                naslov = s.Naslov,
                opis = s.Opis,
                novcani_cilj = s.NovcaniCilj
            });
            var totalItems = _dbContext.Price.Where(k => k.korisnikID != korisnikID && k.Aktivna != false).Count();
            var totalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);
            var odgovor = new
            {
                price,
                totalPages
            };
            return Ok(odgovor);
        }

        [HttpGet]
        public ActionResult GetMyActiveStories(int korisnikID)
        {
            var price = _dbContext.Price.Where(k => k.korisnikID == korisnikID && k.Aktivna!=false).ToList().Select(s => new
            {
                id = s.Id,
                naslov = s.Naslov,
                opis = s.Opis,
                novcani_cilj = s.NovcaniCilj
            }).OrderByDescending(s => s.id);
     
            
            return Ok(price);
        }

        [HttpGet]
        public ActionResult GetSvePriceZaAdmina(int pageNumber = 1, int pageSize = 5)
        {
            var price = _dbContext.Price.ToList().OrderByDescending(p => p.Id).
                Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(s => new
                {
                    id = s.Id,
                    naslov = s.Naslov,
                    opis = s.Opis,
                    novcani_cilj = s.NovcaniCilj
                });
            var totalItems = _dbContext.Price.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var odgovor = new
            {
                price,
                totalPages
            };
            return Ok(odgovor);
        }


        [HttpGet("{korisnikID}")]
        public int BrojAktivnih(int korisnikID)
        {
            var korisnik = _dbContext.Price.Where(k => k.korisnikID == korisnikID && k.Aktivna==true).Count();

            return korisnik;
        }


        [HttpGet]
        public ActionResult GetMyUnActiveStories(int korisnikID)
        {
            var price = _dbContext.Price.Where(k => k.korisnikID == korisnikID && k.Aktivna == false).ToList().Select(s => new
            {
                id = s.Id,
                naslov = s.Naslov,
                opis = s.Opis,
                novcani_cilj = s.NovcaniCilj
            }).OrderByDescending(s => s.id);


            return Ok(price);
        }

        [HttpGet("{pricaid}")]
        public ActionResult GetSlikaPrice(int pricaid)
        {
            byte[]? prica = _dbContext.Price.Find(pricaid)?.Slika;

            if (prica == null)
                return BadRequest();

            return File(prica, "image/*");
        }

        [HttpPost("pricaID")]
        public ActionResult PromijeniSliku([FromBody] PromjenaSlikeVM x)
        { 
            var prica = _dbContext.Price.FirstOrDefault(p => p.Id == x.id);
            prica.Slika = x.slikaKorisnika.ParsirajUbase();
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet("{pricaid}")]
        public ActionResult ObrisiPricu(int pricaid)
        {
          
            var prica = _dbContext.Price.FirstOrDefault(p=>p.Id== pricaid);
            _dbContext.Remove(prica);
            _dbContext.SaveChanges();
            return Ok();
        }



        [HttpGet("{id}")]
        public ActionResult GetByPricaId (int id)
        {
            var prica = _dbContext.Price.Where(x => x.Id == id)
                .Select(p => new 
                {
                    id=p.Id,
                    naslov = p.Naslov,
                    opis = p.Opis,
                    novcaniCilj = p.NovcaniCilj,
                    kategorija = p.Kategorija.Naziv,
                    lokacija = p.Lokacija,
                    korisnik_id = p.korisnikID,
                    imePrezime = p.Korisnik.Ime + " " + p.Korisnik.Prezime
                }).AsQueryable();

            return Ok(prica.ToList());

        }
    }
}
