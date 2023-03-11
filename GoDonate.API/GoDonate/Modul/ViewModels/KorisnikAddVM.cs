namespace GoDonate.Modul.ViewModels
{
    public class KorisnikAddVM
    {
        public int id { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public DateTime datum_rodjenja { get; set; }
        public int grad_id { get; set; }
        public int valuta_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string brojTelefona { get; set; }
        public string slikaKorisnika { get; set; }

    }
}
