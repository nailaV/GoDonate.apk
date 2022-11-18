using System.ComponentModel.DataAnnotations;

namespace GoDonate.Modul.Models
{
    public class Obavijest
    {
        [Key]
        public int ID { get; set; }
        public string Sadrzaj { get; set; }
        public string TipObavijesti { get; set; }
        public DateTime DatumObavjestenja { get; set; }

    }
}
