export interface AutentifikacijaToken {
  id : number;
  vrijednost : string;
  korisnickiNalogId:    number;
  korisnickinalog:      KorisnickiNalog;
  vrijemeEvidentiranja: Date;
  ipAdresa:             string;
}

export class LoginInformacije{
  autentifikacijaToken:AutentifikacijaToken=null;
  isLogiran:boolean=false;
}

export interface KorisnickiNalog{
  id:                 number;
  username:      string;
  slikaKorisnika:    string;
  isAdmin:            boolean;
  brojTelefona:string;
  email:string;
  isKorisnik: boolean;
}
