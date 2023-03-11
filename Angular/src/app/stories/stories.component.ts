import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Konfiguracija} from "../../Config";
import {Router} from "@angular/router";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";
import {LoginInformacije} from "../helperi/login-informacije";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;
@Component({
  selector: 'app-stories',
  templateUrl: './stories.component.html',
  styleUrls: ['./stories.component.scss']
})
export class StoriesComponent implements OnInit {
  prica_podaci: any;
  valutaPodaci: any;
  kategorijaPodaci: any;
  otvoriFormu: boolean = false;
  odabranaPrica: any;
  pageNumber : number = 1;
  korisnikID : number;
  pagingPodaci:any;
  totalPages:number;
  trenutnaStranica:number=1;


  constructor(private httpKlijent: HttpClient, private  router : Router) {
  }

  ngOnInit(): void {
    this.preuzmiPrice();
    this.preuzmiValute();
    this.preuzmiKategorije();
    this.paging();
    this.korisnikID=AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickinalog.id;
    console.log(this.trenutnaStranica);
    console.log(this.pageNumber);
  }
  informacije():LoginInformacije{
    return AutentifikacijaHelper.getLoginInfo();
  }

  preuzmiPrice() {
    this.httpKlijent.get(Konfiguracija.adresaServera + '/Prica/GetSvePrice').subscribe(x => {
      this.prica_podaci = x;
    })
  }

  preuzmiValute() {
    this.httpKlijent.get(Konfiguracija.adresaServera + '/Valuta/GetSveValute').subscribe(x => {
      this.valutaPodaci = x;
    })
  }

  preuzmiKategorije() {
    this.httpKlijent.get(Konfiguracija.adresaServera + '/Kategorija/GetSveKategorije').subscribe(x => {
      this.kategorijaPodaci = x;
    })
  }

  novaPrica() {
    this.odabranaPrica =   {
      id:0,
      naslov:"",
      opis:"",
      slika:"",
      novcani_cilj:"",
      lokacija:"",
      kategorija_id:""
    };
  }


  getSliku(x: any) {
    return `${Konfiguracija.adresaServera}/Prica/GetSlikaPrice/${x.id}`;
  }

  generisiPreview() {
    // @ts-ignore
    var file = document.getElementById("formFile").files[0];
    if (file) {
      var reader = new FileReader();
      let this2=this;
      reader.onload=function ()
      {
        this2.odabranaPrica.slika=reader.result.toString();
      }
      reader.readAsDataURL(file);
    }
  }
  /*SlikaBase64string:string="";*/

  SaveDugme() {
      this.httpKlijent.post(`${Konfiguracija.adresaServera}/Prica/Add`, this.odabranaPrica, Konfiguracija.http_opcije()).subscribe(x=>{
        this.preuzmiPrice();
        this.odabranaPrica=null;
      });

  }

  openDetails(x: any) {
    this.router.navigate(['/storyDetails', x.id]);
  }

  tvojePrice() {
    this.prica_podaci=null;
      this.httpKlijent.get(Konfiguracija.adresaServera + '/Prica/GetKorisnikovePrice/' + this.korisnikID).subscribe(x=>{
          this.prica_podaci=x;
      })
  }

  drugePrice() {
        this.prica_podaci=null;
        this.httpKlijent.get(Konfiguracija.adresaServera + '/Prica/GetOstalePrice/' +this.korisnikID).subscribe(x=>{
          this.prica_podaci=x;
        })
  }

  paging() {
      this.httpKlijent.get(Konfiguracija.adresaServera+
        '/Prica/GetPricePaging?pageNumber=' + this.pageNumber + '&pageSize=5').subscribe(x=>{
        this.pagingPodaci=x;
        this.totalPages=this.pagingPodaci.totalPages;
        console.log(this.totalPages);
        console.log(this.pagingPodaci);
      });
  }

  prethodnaStranica() {
    this.trenutnaStranica--;
    this.pageNumber--;
      this.paging();
  }

  sljedecaStranica() {
      this.trenutnaStranica++;
    this.pageNumber++;
    this.paging();
  }
}
