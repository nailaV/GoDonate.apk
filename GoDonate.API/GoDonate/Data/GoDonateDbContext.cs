using GoDonate.Modul.Autentifikacija;
using GoDonate.Modul.Models;
using Microsoft.EntityFrameworkCore;

namespace GoDonate.Data
{
    public class GoDonateDbContext:DbContext
    {
        public GoDonateDbContext(DbContextOptions opcije) : base(opcije)
        {

        }

        public DbSet<Drzava> Drzave { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<Prica> Price { get; set; }
        public DbSet<KorisnickiNalog> KorisnickiNalozi { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Administrator> Administratori { get; set; }
        public DbSet<Kartica> Kartice { get; set; }
        public DbSet<Donacija> Donacije { get; set; }
        public DbSet<Valuta> Valute { get; set; }
        public DbSet<Jezik> Jezici { get; set; }
        public DbSet<Komentar> Komentari { get; set; }
        public DbSet<Obavijest> Obavijesti { get; set; }
        public DbSet<Poruka> Poruke { get; set; }
        public DbSet<AutentifikacijaToken> AutentifikacijaToken { get; set; }
    }
}
