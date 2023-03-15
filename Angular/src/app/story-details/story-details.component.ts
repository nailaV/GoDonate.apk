import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {Konfiguracija} from "../../Config";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";
import {LoginInformacije} from "../helperi/login-informacije";
declare function porukaSuccess(a: string):any;
@Component({
  selector: 'app-story-details',
  templateUrl: './story-details.component.html',
  styleUrls: ['./story-details.component.scss']
})
export class StoryDetailsComponent implements OnInit{
  pricaId : number;
  podaciPrica : any;
  ukupnoPrica:any;
  zaProgressBar : any;
  informacije():LoginInformacije{
    return AutentifikacijaHelper.getLoginInfo();
  }



  constructor(private httpKlijent : HttpClient, private router : ActivatedRoute, private rut : Router) {
  }


  ngOnInit() {
    this.router.params.subscribe(params=>{
      this.pricaId=+params['storyId'];
      this.fetchPricaById();
      this.getUkupno();
    })
  }

  fetchPricaById() {
      this.httpKlijent.get(Konfiguracija.adresaServera + '/Prica/GetByPricaId/'+this.pricaId)
        .subscribe(x=>{
          this.podaciPrica=x;
        })
  }
  getSliku(x: number) {
    return `${Konfiguracija.adresaServera}/Prica/GetSlikaPrice/${x}`;
  }

  obrisiPricu(id:number) {
    this.httpKlijent.get(Konfiguracija.adresaServera + '/Prica/ObrisiPricu/' + id).subscribe(x=>{
        porukaSuccess('uspjesno obrisana priča');
        this.rut.navigateByUrl('stories');
    })
  }

  getUkupno() {
    this.httpKlijent.get(Konfiguracija.adresaServera + '/Donacija/GetUkupnoZaPricu/' + this.pricaId).subscribe(x=>{
      this.ukupnoPrica=x;
    })
  }

  openDonation(x: any) {
    this.rut.navigate(['/donation', x.id]);
  }

}
