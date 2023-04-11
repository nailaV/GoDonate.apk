import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {Konfiguracija} from "../../Config";
declare function porukaSuccess(a: string):any;

@Component({
  selector: 'app-administrator',
  templateUrl: './administrator.component.html',
  styleUrls: ['./administrator.component.scss']
})
export class AdministratorComponent implements OnInit{
  korisnikPodaci: any;
  odabraniKorisnik: any;

  constructor(private httpKlijent : HttpClient, private router : Router) {
  }

  ngOnInit(): void {
    this.fetchSveKorisnike();
  }


  fetchSveKorisnike() {
    this.httpKlijent.get(Konfiguracija.adresaServera + '/Korisnik/GetSveKorisnike').subscribe(x=>{
      this.korisnikPodaci=x;
    })
  }


  izbrisi(id:number) {
      this.httpKlijent.post(Konfiguracija.adresaServera+'/Administrator/ObrisiKorisnika/' + id,null).subscribe(x=>{
        porukaSuccess('Successfully deleted user!');
        this.fetchSveKorisnike();
        this.odabraniKorisnik=null;
      })
  }
}
