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
  brojKomentara: any;
  moneyGoal: number;
  currentMoneyDonated: number;
  percentageComplete: number;

  informacije():LoginInformacije{
    return AutentifikacijaHelper.getLoginInfo();
  }

  constructor(private httpKlijent : HttpClient, private router : ActivatedRoute, private rut : Router) {
  }


  ngOnInit() {
    this.router.params.subscribe(params=>{
      this.pricaId=+params['storyId'];
      this.fetchPricaById();
      this.getBrojKomentara();
      this.getUkupnoDonirano();
      this.getMoneyGoal();
    })
  }

  fetchPricaById() {
      this.httpKlijent.get(Konfiguracija.adresaServera + '/Prica/GetByPricaId/'+this.pricaId)
        .subscribe(x=>{
          this.podaciPrica=x;
          this.moneyGoal=this.podaciPrica.novcaniCilj;
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


  openDonation(x: any) {
    this.rut.navigate(['/donation', x.id]);
  }

   getBrojKomentara() {
      this.httpKlijent.get(Konfiguracija.adresaServera + '/Komentar/GetBrojKomentara/' + this.pricaId).subscribe(x=>{
        this.brojKomentara=x;
      })
  }

  otvoriKomentare() {
    this.rut.navigate(['komentari',this.pricaId]);
  }

  vratiNazad() {
    this.rut.navigateByUrl('/stories');
  }


  getUkupnoDonirano() {
      this.httpKlijent.get(Konfiguracija.adresaServera + '/Donacija/GetUkupnoZaFormulu/' + this.pricaId).subscribe((x:any)=>{
        this.currentMoneyDonated=x;
        if(this.currentMoneyDonated==0)
          this.percentageComplete=0;
      })
  }

   getMoneyGoal() {
     this.httpKlijent.get(Konfiguracija.adresaServera + '/Prica/GetMoneyGoal/' + this.pricaId).subscribe((x:any)=>{
       this.moneyGoal=x;
       this.percentageComplete= Math.round((this.currentMoneyDonated / this.moneyGoal) * 100);
       console.log(typeof(this.moneyGoal));
       console.log(typeof(this.currentMoneyDonated));
       console.log(typeof(this.percentageComplete));
     })
  }
}
