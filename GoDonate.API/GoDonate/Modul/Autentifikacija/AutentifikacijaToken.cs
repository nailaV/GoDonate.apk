using GoDonate.Modul.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoDonate.Modul.Autentifikacija
{
    public class AutentifikacijaToken
    {
        [Key]
        public int id { get; set; }
        public string vrijednost { get; set; }
        [ForeignKey(nameof(korisnickinalog))]
        public int korisnickinalogID { get; set; }
        public KorisnickiNalog korisnickinalog { get; set; }
        public DateTime vrijemeEvidencije { get; set; }
        public string ipAdresa { get; set; }
    }
}
