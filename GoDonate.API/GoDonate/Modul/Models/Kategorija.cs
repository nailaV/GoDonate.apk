using System.ComponentModel.DataAnnotations;

namespace GoDonate.Modul.Models
{
    public class Kategorija
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
    }
}
