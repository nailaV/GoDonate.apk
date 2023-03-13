using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoDonate.Modul.Models
{
    public class Kartica
    {
        [Key]
        public int Id { get; set; }
        public int BrojKartice { get; set; }
        public string TipKartice { get; set; }
        public int CVV_CVC { get; set; }
        public DateTime? DatumVazenja { get; set; }
        public int? MjesecIsteka { get; set; }   
        public int? GodinaIsteka { get; set; }   
        [ForeignKey(nameof(KorisnikID))]
        public Korisnik Korisnik { get; set; }
        public int KorisnikID { get; set; }
    }
}
