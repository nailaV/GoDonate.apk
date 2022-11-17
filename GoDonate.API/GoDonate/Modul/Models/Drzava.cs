using System.ComponentModel.DataAnnotations;

namespace GoDonate.Modul.Models
{
    public class Drzava
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NazivDrzave { get; set; }
        public string? Skracenica { get; set; }
    }
}
