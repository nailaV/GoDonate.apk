import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {Konfiguracija} from "../../Config";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";
declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;
@Component({
  selector: 'app-donation',
  templateUrl: './donation.component.html',
  styleUrls: ['./donation.component.scss']
})
export class DonationComponent implements OnInit{
  storyID:any;
  pricaPodaci:any;
  kartica:any;
  broj:any;
  karticaPodatak:any;

  constructor(private httpKlijent : HttpClient, private router : ActivatedRoute, private rut : Router) {
  }

  ngOnInit() {
    this.router.params.subscribe(params=>{
      this.storyID=+params['storyID'];
      this.getPodaciPrice();
      this.countKartica()
      this.karticaPodaci();
    })
  }

  private getPodaciPrice() {
    this.httpKlijent.get(Konfiguracija.adresaServera+"/Prica/GetByPricaId/"+this.storyID).subscribe(x=>{
      this.pricaPodaci=x;
    })
  }


  novaKartica() {
    this.kartica={
      id:0,
      brojKartice:"",
      tipKartice:"",
      cvv:"",
      datumVazenja:new Date(),
      korisnikID:AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickinalog.id
    }
  }

  dodajKarticu() {
    this.httpKlijent.post(Konfiguracija.adresaServera+"/Kartica/AddKarticu", this.kartica).subscribe(x=>{
      porukaSuccess("Successfully added new card.");
      this.kartica=null;
      this.rut.navigateByUrl('/donation');
    })
  }

   countKartica() {
    this.httpKlijent.get(Konfiguracija.adresaServera+'/Kartica/CountKartica/'+AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickinalog.id).subscribe(x=>{
      this.broj=x;
    })
  }

  karticaPodaci() {
    this.httpKlijent.get(Konfiguracija.adresaServera+'/Kartica/GetKorisnikoveKartice/'+AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickinalog.id).subscribe(x=>{
      this.karticaPodatak=x;
    })
  }
}


