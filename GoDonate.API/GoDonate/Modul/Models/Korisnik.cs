using System.ComponentModel.DataAnnotations.Schema;

namespace GoDonate.Modul.Models
{
    [Table("Korisnici")]
    public class Korisnik : KorisnickiNalog
    {
        public string Ime { get; set; }

        public string Prezime { get; set; }

        public DateTime DatumRodjenja { get; set; }

        [ForeignKey(nameof(gradID))]
        public Grad Grad { get; set; }
        public int gradID { get; set; }

        [ForeignKey(nameof(valutaID))]
        public Valuta Valuta { get; set; }
        public int valutaID { get; set; }
        public bool isAktivan { get; set; } = false;
        public string? Token { get; set; }

    }
}
