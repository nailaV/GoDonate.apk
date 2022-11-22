using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoDonate.Modul.Models
{
    public class Drzava
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NazivDrzave { get; set; }
        public string? Skracenica { get; set; }
        [ForeignKey(nameof(valutaID))]
        public Valuta Valuta { get; set; }
        public int valutaID { get; set; }
    }
}
