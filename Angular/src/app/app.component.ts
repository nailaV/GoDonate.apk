import { Component, OnInit} from '@angular/core';

import {Konfiguracija} from "./Config";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Angular';
  drzava_podaci : any;

  constructor(private httpKlijent : HttpClient) {
  }

  ngOnInit(): void {
    this.fetchPodaci();
  }


fetchPodaci(){
    this.httpKlijent.get(Konfiguracija.adresa + '/Drzava/GetSveDrzave').subscribe((x:any)=>{
      this.drzava_podaci=x;
    })
}
}
