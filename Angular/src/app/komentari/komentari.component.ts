import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {Konfiguracija} from "../../Config";
import {LoginInformacije} from "../helperi/login-informacije";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
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
  lajkButton : boolean = false;
  dislajkButton : boolean = false;
  validiraj:FormGroup;

  constructor(private httpKlijent : HttpClient, private router : Router, private activated : ActivatedRoute, private formBuilder:FormBuilder) {
  this.validiraj=this.formBuilder.group({
    sadrzaj: new FormControl('', [
      Validators.required])
  })
  }

  get sadrzaj() : FormControl{
    return this.validiraj.get("sadrzaj") as FormControl;
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
    if(this.validiraj.valid)
    {
      let sadrzaj = {
        pricaID : this.pricaID,
        korisnikID : this.informacije().autentifikacijaToken.korisnickinalog.id,
        ...this.validiraj.value
      }
      this.httpKlijent.post
      (Konfiguracija.adresaServera + '/Komentar/AddKomentar',sadrzaj)
        .subscribe(x=>{
          porukaSuccess('Successfully commented on the story.');
          this.ucitajKomentare();
          this.validiraj.get('sadrzaj').setValue(null);
        })
    }
    else{
      porukaError('Registration not confirmed. Please try again.');
    }
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
  lajkaj(id:number) {
    this.httpKlijent.post(Konfiguracija.adresaServera+'/Komentar/Like/' + id,{}).subscribe(x=>{
      this.ucitajKomentare();
      this.lajkButton=true;
    })
  }
  dislajkaj(id:number) {
    this.httpKlijent.post(Konfiguracija.adresaServera + '/Komentar/Dislike/' + id, {}).subscribe(x => {
      this.ucitajKomentare();
      this.dislajkButton=true;
    })
  }
    getSlika(korisnikID: number) {
      return `${Konfiguracija.adresaServera}/Korisnik/GetSlikuKorisnika/${korisnikID}`;
    }

  obrisiKomentar(id:number) {
      this.httpKlijent.post(Konfiguracija.adresaServera + '/Komentar/Obrisi/' + id,{}).subscribe(s=>{
        this.ucitajKomentare();
      })
  }

  vratiNazad() {
    this.router.navigate(['storyDetails',this.pricaID]);
  }
}
