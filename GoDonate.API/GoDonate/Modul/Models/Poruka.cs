using System.ComponentModel.DataAnnotations.Schema;

namespace GoDonate.Modul.Models
{
    public class Poruka
    {
        public int ID { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime Datum { get; set; }
        [ForeignKey(nameof(korisnikID))]
        public Korisnik Korisnik { get; set; }
        public int korisnikID { get; set; }
    }
}
