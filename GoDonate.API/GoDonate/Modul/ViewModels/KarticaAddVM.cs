namespace GoDonate.Modul.ViewModels
{
    public class KarticaAddVM
    {
        public int id { get; set; }
        public int brojKartice { get; set; }
        public string tipKartice { get; set; }
        public int cvv { get; set; }
        public DateTime datumVazenja { get; set; }
        public int korisnikID { get; set; }
    }
}
