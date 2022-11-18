using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoDonate.Modul.Models
{
    public class Valuta
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Skracenica { get; set; }
        
    }
}
