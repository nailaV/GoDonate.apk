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
    }
}
