import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Konfiguracija} from "../../Config";
import {Router} from "@angular/router";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";
import {LoginInformacije} from "../helperi/login-informacije";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Location} from "@angular/common";
import {SignalRServis} from "../SignalR/SignalRService";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;
@Component({
  selector: 'app-stories',
  templateUrl: './stories.component.html',
  styleUrls: ['./stories.component.scss']
})
export class StoriesComponent implements OnInit {
  otvoriTudje: boolean = true;
  otvoriMoje: boolean = false;
  prica_tudja: any;
  prica_moja: any;
  prica_mojaNeaktivna: any;
  prica_mojaAktivna: any;
  valutaPodaci: any;
  kategorijaPodaci: any;
  otvoriFormu: boolean = false;
  odabranaPrica: any;
  pageNumber: number = 1;
  korisnikID: number;
  pagingPodaci: any;
  totalPages: number;
  trenutnaStranica: number = 1;
  validiraj: FormGroup;
  brojAktivnih: any;
  zaAdmina: any;
  TopDonatoriVarijabla: any[];
  prviDonator: any;
  drugiDonator: any;
  treciDonator: any;



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

  constructor(private httpKlijent: HttpClient, private  router : Router,private formBuilder:FormBuilder, signalR : SignalRServis) {
    this.validiraj=this.formBuilder.group({
      naslov:new FormControl('', [
        Validators.required,
        Validators.pattern('[a-zA-Z ]*')]),
      opis:new FormControl('', [
        Validators.required,
        Validators.pattern('[a-zA-Z ]*')]),
      slika:new FormControl('', [
        Validators.required]),
      novcani_cilj:new FormControl('', [
        Validators.required]),
      lokacija:new FormControl('', [
        Validators.required,
        Validators.pattern('[a-zA-Z ]*')]),
      kategorija_id:new FormControl('', [
        Validators.required])
    })
    signalR.pokreniKonekciju();
  }

  ngOnInit(): void {
    this.korisnikID=AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickinalog.id;
      this.getStatus();
      this.preuzmiTudje();
      this.TopDonatori();
      this.preuzmiValute();
      this.preuzmiKategorije();
      this.preuzmiBroj();
  }
  informacije():LoginInformacije{
    return AutentifikacijaHelper.getLoginInfo();
  }
  getSvePrice(){
    this.httpKlijent.get(Konfiguracija.adresaServera+'/Prica/GetSvePrice').subscribe(x=>{
          this.zaAdmina=x;
    })
  }

  preuzmiTudje() {
    this.prica_mojaNeaktivna=null;
    this.prica_mojaAktivna=null;
    this.otvoriTudje=true;
    this.otvoriMoje=false;
    this.httpKlijent.get(Konfiguracija.adresaServera + '/Prica/GetOtherStoriesPaging?korisnikID=' +
      this.korisnikID + '&pageNumber=' + this.pageNumber + '&pageSize=5').subscribe(x => {
      this.prica_tudja = x;
      this.totalPages=this.prica_tudja.totalPages;
      document.getElementById('prvi').style.color='white';
      document.getElementById('prvi').style.borderStyle='solid';
      document.getElementById('drugi').style.color='black';
      document.getElementById('drugi').style.border='none';
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
  signal: boolean=false;

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
        this.preuzmiMojeAktivne();
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


  prethodnaStranica() {
    this.trenutnaStranica--;
    this.pageNumber--;
      this.preuzmiTudje();
  }

  sljedecaStranica() {
      this.trenutnaStranica++;
    this.pageNumber++;
    this.preuzmiTudje();
  }

  preuzmiMojeAktivne() {
    this.totalPages=0;
    this.trenutnaStranica=0;
    this.prica_tudja=null;
    this.otvoriTudje=false;
    this.otvoriMoje=true;
    this.prica_mojaNeaktivna=null;
    this.httpKlijent.get(Konfiguracija.adresaServera + '/Prica/GetMyActiveStories?korisnikID=' +
      this.korisnikID).subscribe(x => {
      this.prica_mojaAktivna = x;
      document.getElementById('prvi').style.color='black';
      document.getElementById('prvi').style.border='none';
      document.getElementById('drugi').style.color='white';
      document.getElementById('drugi').style.border='solid';
      document.getElementById('aktivnePrice').style.color='white';
      document.getElementById('neaktivnePrice').style.color='black';
    })
  }

  preuzmiMojeNeaktivne() {
    this.totalPages=0;
    this.trenutnaStranica=0;
    this.prica_tudja=null;
    this.prica_mojaAktivna=null;
    this.httpKlijent.get(Konfiguracija.adresaServera + '/Prica/GetMyUnActiveStories?korisnikID=' +
      this.korisnikID ).subscribe(x => {
      this.prica_mojaNeaktivna = x;
      document.getElementById('aktivnePrice').style.color='black';
      document.getElementById('neaktivnePrice').style.color='white';
    })
  }

   preuzmiBroj() {
      this.httpKlijent.get(Konfiguracija.adresaServera + '/Prica/BrojAktivnih/' + this.korisnikID).subscribe(x=>{
        this.brojAktivnih=x;
      })
  }

  getStatus() {
    this.httpKlijent.get(Konfiguracija.adresaServera+'/Donacija/GetUkupnoZaPrice').subscribe();
  }

  TopDonatori() {
      this.httpKlijent.get<any[]>(Konfiguracija.adresaServera + '/Donacija/GetTopDonators').subscribe(x=>{
        this.TopDonatoriVarijabla=x;
        this.prviDonator=this.TopDonatoriVarijabla[0];
        this.drugiDonator=this.TopDonatoriVarijabla[1];
        this.treciDonator=this.TopDonatoriVarijabla[2];
        console.log(this.prviDonator);
        console.log(this.drugiDonator);
        console.log(this.treciDonator);
      })
  }
}
