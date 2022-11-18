using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoDonate.Modul.Models
{
    public class Komentar
    {
        [Key]
        public int Id { get; set; }
        public string Sadrzaj { get; set; }
        [ForeignKey(nameof(pricaID))]
        public Prica Prica { get; set; }
        public int pricaID { get; set; }
    }
}
