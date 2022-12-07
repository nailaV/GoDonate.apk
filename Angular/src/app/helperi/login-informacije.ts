
export interface AutentifikacijaToken {
  id : number;
  vrijednost : string;
  korisnickiNalogId:    number;
  korisnickiNalog:      KorisnickiNalog;
  vrijemeEvidentiranja: Date;
  ipAdresa:             string;
}

export class LoginInformacije{
  autentifikacijaToken:AutentifikacijaToken=null;
  isLogiran:boolean=false;
}

export interface KorisnickiNalog{
  id:                 number;
  korisnickoIme:      string;
  slika_korisnika:    string;
  isAdmin:            boolean;
  isKorisnik: boolean;
}
