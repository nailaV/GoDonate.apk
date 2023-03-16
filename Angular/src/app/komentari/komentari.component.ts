import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {Konfiguracija} from "../../Config";
import {LoginInformacije} from "../helperi/login-informacije";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";
declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;
@Component({
  selector: 'app-komentari',
  templateUrl: './komentari.component.html',
  styleUrls: ['./komentari.component.scss']
})
export class KomentariComponent implements  OnInit{
  pricaID : any;
  komentariPodaci : any;
  sadrzajKomentara:any;
  trenutnaStranica:number = 1;
  totalPages : number;
  pageNumber : number = 1;
  constructor(private httpKlijent : HttpClient, private router : Router, private activated : ActivatedRoute) {

  }

  informacije():LoginInformacije{
    return AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    this.activated.params.subscribe(params=>{
      this.pricaID = +params['storyID'];
      console.log(this.pricaID);
    })
    this.ucitajKomentare();
  }

   ucitajKomentare() {
      this.httpKlijent.get
      (Konfiguracija.adresaServera + '/Komentar/GetKomentareZaPricu?pricaID=' + this.pricaID +
      '&pageNumber=' + this.pageNumber + '&pageSize=5')
        .subscribe(x=>{
          this.komentariPodaci = x;
          this.totalPages=this.komentariPodaci.totalPages;
      })
  }

  dodajKomentar() {
      let sadrzaj = {
        pricaID : this.pricaID,
        korisnikID : this.informacije().autentifikacijaToken.korisnickinalog.id,
        sadrzaj : this.sadrzajKomentara
      }
      this.httpKlijent.post
      (Konfiguracija.adresaServera + '/Komentar/AddKomentar',sadrzaj)
        .subscribe(x=>{
          porukaSuccess('Successfully commented on the story.');
          this.ucitajKomentare();
          this.sadrzajKomentara="";
      })
  }

  prethodna() {
      this.trenutnaStranica--;
      this.pageNumber--;
      this.ucitajKomentare();
  }
  sljedeca(){
      this.trenutnaStranica++;
      this.pageNumber++;
      this.ucitajKomentare();
  }
}
