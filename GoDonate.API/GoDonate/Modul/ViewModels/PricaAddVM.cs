﻿namespace GoDonate.Modul.ViewModels
{
    public class PricaAddVM
    {
        public string naslov { get; set; }
        public string opis { get; set; }
        public byte[] slika { get; set; }
        public int novcani_cilj { get; set; }
        public string lokacija { get; set; }
        public int kategorija_id { get; set; }
        public int korisnik_id { get; set; }
    }
}