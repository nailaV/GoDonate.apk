import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Konfiguracija} from "../../Config";
import {Router} from "@angular/router";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";
import {LoginInformacije} from "../helperi/login-informacije";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";

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
  validiraj:FormGroup;

  get naslov() : FormControl{
    return this.validiraj.get("naslov") as FormControl;
  }
  get opis() : FormControl{
    return this.validiraj.get("opis") as FormControl;
  }
  get slika() : FormControl{
    return this.validiraj.get("slika") as FormControl;
  }
  get novcani_cilj() : FormControl{
    return this.validiraj.get("novcani_cilj") as FormControl;
  }
  get lokacija() : FormControl{
    return this.validiraj.get("lokacija") as FormControl;
  }
  get kategorija_id() : FormControl{
    return this.validiraj.get("kategorija_id") as FormControl;
  }

  constructor(private httpKlijent: HttpClient, private  router : Router,private formBuilder:FormBuilder) {
    this.validiraj=this.formBuilder.group({
      naslov:new FormControl('', [
        Validators.required,
        Validators.pattern('[a-zA-Z]*')]),
      opis:new FormControl('', [
        Validators.required,
        Validators.pattern('[a-zA-Z]*')]),
      slika:new FormControl('', [
        Validators.required]),
      novcani_cilj:new FormControl('', [
        Validators.required]),
      lokacija:new FormControl('', [
        Validators.required,
        Validators.pattern('[a-zA-Z]*')]),
      kategorija_id:new FormControl('', [
        Validators.required])
    })
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

  public slikab64:any;

  generisiPreview(event:any) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    let this2=this;
    reader.onload=()=>{
      this2.slikab64 = reader.result;
    };
    }


  SaveDugme() {
    if(this.validiraj.valid){
      let s={
        id:0,
        korisnik_id:AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickinalog.id,
        ...this.validiraj.value,
        slika:this.slikab64
      };
      this.httpKlijent.post(`${Konfiguracija.adresaServera}/Prica/Add`, s, Konfiguracija.http_opcije()).subscribe(x=>{
        porukaSuccess("Story successfully added. Good luck with collecting money!")
        this.preuzmiPrice();
        this.odabranaPrica=null;
      })
    }
    else{
      porukaError('Failed to add story. Please try again.');
    }

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
