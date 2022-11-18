using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoDonate.Modul.Models
{
    public class Jezik
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        [ForeignKey(nameof(korisnikID))]
        public Korisnik Korisnik { get; set; }
        public int korisnikID { get; set; }
    }
}
