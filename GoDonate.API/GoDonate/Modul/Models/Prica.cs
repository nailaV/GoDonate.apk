using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoDonate.Modul.Models
{
    public class Prica
    {
        [Key]
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string Opis { get; set; }
        public byte[] Slika { get; set; }
        public int NovcaniCilj { get; set; }
        public string Lokacija { get; set; }

        [ForeignKey(nameof(kategorijaID))]
        public Kategorija Kategorija { get; set; }
        public int kategorijaID { get; set; }
    }
}
