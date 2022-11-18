using System.ComponentModel.DataAnnotations.Schema;

namespace GoDonate.Modul.Models
{
    [Table("Administratori")]
    public class Administrator : KorisnickiNalog
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
    }
}
