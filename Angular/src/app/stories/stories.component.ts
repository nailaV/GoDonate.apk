import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Konfiguracija} from "../../Config";

@Component({
  selector: 'app-stories',
  templateUrl: './stories.component.html',
  styleUrls: ['./stories.component.scss']
})
export class StoriesComponent implements OnInit {
  porukaUspjesno:boolean=true;
  prica_podaci: any;
  valutaPodaci: any;
  kategorijaPodaci: any;
  otvoriFormu:boolean=false;
  prica:any;


  constructor(private httpKlijent : HttpClient) {
  }

  ngOnInit(): void {
    this.preuzmiPrice();
    this.preuzmiValute();
    this.preuzmiKategorije();
  }

  preuzmiPrice(){
      this.httpKlijent.get(Konfiguracija.adresaServera + '/Prica/GetSvePrice').subscribe(x=>{
        this.prica_podaci=x;
      })
  }

   preuzmiValute() {
     this.httpKlijent.get(Konfiguracija.adresaServera + '/Valuta/GetSveValute').subscribe(x=>{
       this.valutaPodaci=x;
     })
  }

  preuzmiKategorije() {
    this.httpKlijent.get(Konfiguracija.adresaServera + '/Kategorija/GetSveKategorije').subscribe(x=>{
      this.kategorijaPodaci=x;
    })
  }

  spasiPricu(){
    this.prica={

    }
  }

  getSliku(x: any) {
    return `${Konfiguracija.adresaServera}/Prica/GetSlikaPrice/${x.id}`;
  }
}
