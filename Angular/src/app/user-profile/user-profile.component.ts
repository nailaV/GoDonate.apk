import {Component, OnInit} from '@angular/core';
import {Konfiguracija} from "../../Config";
import {LoginInformacije} from "../helperi/login-informacije";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent  implements OnInit{

  constructor(private httpKlijent: HttpClient, private router :Router) {
  }
  otvoriFormu2:boolean=false;
  otvoriFormu:boolean=false;
  novaSlika:any;
  novaSifra:any;

  getSlikuKorisnika(id:number) {
    return `${Konfiguracija.adresaServera}/Korisnik/GetSlikuKorisnika/${id}`;
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    this.novaSlika={id:this.loginInfo().autentifikacijaToken.korisnickinalog.id, slikaKorisnika:""};
    this.novaSifra={id:this.loginInfo().autentifikacijaToken.korisnickinalog.id,
    stariPassword:"", noviPassword:""};
  }

  SaveDugme() {
  this.httpKlijent.post(`${Konfiguracija.adresaServera}/Korisnik/PromjeniSliku`, this.novaSlika, Konfiguracija.http_opcije()).subscribe(x=>{
      this.otvoriFormu=false;
    });

  }

  generisiPreview() {
    // @ts-ignore
    var file = document.getElementById("formFile").files[0];
    if (file) {
      var reader = new FileReader();
      let this2=this;
      reader.onload=function ()
      {
        this2.novaSlika.slikaKorisnika=reader.result.toString();
      }
      reader.readAsDataURL(file);
    }
  }

  PromijeniPassword() {
    this.httpKlijent.post(`${Konfiguracija.adresaServera}/Korisnik/PromjeniPassword`, this.novaSifra, Konfiguracija.http_opcije()).subscribe(x=>{
      this.otvoriFormu2=false;
      AutentifikacijaHelper.setLoginInfo(x=null);
      this.router.navigateByUrl("/logIn");
    });
  }
}
