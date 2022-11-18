using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoDonate.Modul.Models
{
    public class Donacija
    {
        [Key]
        public int ID { get; set; }
        public int KolicinaNovca { get; set; }
        public DateTime Datum { get; set; }
        [ForeignKey(nameof(karticaID))]
        public Kartica Kartica { get; set; }
        public int karticaID { get; set; }
        [ForeignKey(nameof(pricaID))]
        public Prica Prica { get; set; }
        public int pricaID { get; set; }
    }
}
