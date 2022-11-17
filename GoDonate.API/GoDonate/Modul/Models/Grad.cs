using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoDonate.Modul.Models
{
    public class Grad
    {
        [Key]
        public int GradID { get; set; }
        public string Naziv { get; set; }
        public int PostanskiBroj { get; set; }

        [ForeignKey(nameof(drzavaID))]
        public Drzava Drzava { get; set; }
        public int drzavaID { get; set; }
    }
}
