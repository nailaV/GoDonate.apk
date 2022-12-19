import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Konfiguracija} from "../../Config";
import {Router} from "@angular/router";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit{

  constructor(private httpKlijent: HttpClient,  private router : Router ) {
  }
  ngOnInit(): void {
    this.korisnik={
      id:0,
      ime:"",
      prezime:"",
      datum_rodjenja:"2022-12-18T20:23:57.848Z",
      email:"",
      username:"",
      password:"",
      brojTelefona:"",
      slikaKorisnika:"",
      grad_id:"",
      valuta_id:1
    }
    this.preuzmiGradove();
  }
  korisnik: any;
  gradoviPodaci:any;


  preuzmiGradove() {
    this.httpKlijent.get(Konfiguracija.adresaServera + '/Grad/GetSviGradovi').subscribe(x => {
      this.gradoviPodaci= x;
    })
  }

  generisiPreview() {
    // @ts-ignore
    var file = document.getElementById("formFile").files[0];
    if (file) {
      var reader = new FileReader();
      let this2 = this;
      reader.onload = function () {
        this2.korisnik.slikaKorisnika = reader.result.toString();
      }
      reader.readAsDataURL(file);
    }
  }

  RegisterDugme() {
    this.httpKlijent.post(`${Konfiguracija.adresaServera}/Korisnik/Add`, this.korisnik, Konfiguracija.http_opcije()).subscribe(x=>{
      this.router.navigateByUrl('/logIn');
    });
  }
}
