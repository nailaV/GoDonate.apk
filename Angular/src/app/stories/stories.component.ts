import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Konfiguracija} from "../../Config";
import {Router} from "@angular/router";

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


  constructor(private httpKlijent: HttpClient, private  router : Router) {
  }

  ngOnInit(): void {
    this.preuzmiPrice();
    this.preuzmiValute();
    this.preuzmiKategorije();
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
}
