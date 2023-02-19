namespace GoDonate.Modul.ViewModels
{
    public class KorisnikGetAllVM
    {
        public int id { get; set; }
        public string  ime { get; set; }
        public string prezime { get; set; }
        public int? opstina_rodjenja_id { get; set; }
        public string opstina_rodjenja_opis { get; set; }
        public string drzava_rodjenja_opis { get; set; }
        public DateTime datum_rodj { get; set; }
        public string email { get; set; }
        public string brojTelefona { get; set; }
        public string username { get; set; }
    }
}
