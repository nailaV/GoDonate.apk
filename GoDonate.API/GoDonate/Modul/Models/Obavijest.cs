using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoDonate.Modul.Models
{
    public class Obavijest
    {
        [Key]
        public int ID { get; set; }
        public string Sadrzaj { get; set; }
        public string TipObavijesti { get; set; }
        public DateTime DatumObavjestenja { get; set; }
        [ForeignKey(nameof(korisnikID))]
        public Korisnik Korisnik { get; set; }
        public int korisnikID { get; set; }

    }
}
