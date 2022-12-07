﻿using GoDonate.Modul.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoDonate.Modul.Autentifikacija
{
    public class SistemskoKretanje
    {
        [Key]
        public int id { get; set; }
        [ForeignKey(nameof(korisnik))]
        public int korisnikID { get; set; }
        public KorisnickiNalog korisnik { get; set; }
        public string queryPath { get; set; }
        public string postData { get; set; }
        public DateTime vrijeme { get; set; }
        public string ipAdresa { get; set; }
        public string exceptionMessage { get; set; }
        public bool isException { get; set; }
    }
}
