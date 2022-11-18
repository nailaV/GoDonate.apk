using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoDonate.Modul.Models
{
    [Table("KorisnickiNalozi")]
    public class KorisnickiNalog
    {
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Slika { get; set; }
    }
}
