using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GoDonate.Modul.Models
{
    [Table("KorisnickiNalozi")]
    public class KorisnickiNalog
    {
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public Korisnik korisnik => this as Korisnik;
        [JsonIgnore]
        public Administrator administrator => this as Administrator;

        public bool isKorisnik => korisnik != null;

        public bool isAdmin => administrator != null;
        public string Email { get; set; }
        public string BrojTelefona { get; set; }
        public byte[]? SlikaKorisnika { get; set; }

    }
}
