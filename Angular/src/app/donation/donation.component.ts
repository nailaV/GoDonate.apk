import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {Konfiguracija} from "../../Config";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;
@Component({
  selector: 'app-donation',
  templateUrl: './donation.component.html',
  styleUrls: ['./donation.component.scss']
})
export class DonationComponent implements OnInit{
  storyID:any;
  pricaPodaci:any;
  kartica:any;
  broj:any;
  karticaPodatak:any;
  donacijaPodaci:any;
  validiraj:FormGroup;
  validacijaDonacije:FormGroup;

  get brojKartice() : FormControl{
    return this.validiraj.get("brojKartice") as FormControl;
  }
  get tipKartice() : FormControl{
    return this.validiraj.get("tipKartice") as FormControl;
  }
  get cvv() : FormControl{
    return this.validiraj.get("cvv") as FormControl;
  }
  get mjesecVazenja() : FormControl{
    return this.validiraj.get("mjesecVazenja") as FormControl;
  }
  get godinaVazenja() : FormControl{
    return this.validiraj.get("godinaVazenja") as FormControl;
  }


  get kartica_id() : FormControl{
    return this.validacijaDonacije.get("kartica_id") as FormControl;
  }
  get kolicina_novca() : FormControl{
    return this.validacijaDonacije.get("kolicina_novca") as FormControl;
  }


  constructor(private httpKlijent : HttpClient, private router : ActivatedRoute, private rut : Router,  private formBuilder:FormBuilder) {
    this.validacijaDonacije=this.formBuilder.group({
      kartica_id:new FormControl('', [
        Validators.required
      ]),
      kolicina_novca:new FormControl('',[
        Validators.required
      ])
    })
  }

  ngOnInit() {
    this.router.params.subscribe(params=>{
      this.storyID=+params['storyID'];
      this.getPodaciPrice();
      this.countKartica()
      this.karticaPodaci();
    })
  }

  getPodaciPrice() {
    this.httpKlijent.get(Konfiguracija.adresaServera+"/Prica/GetByPricaId/"+this.storyID).subscribe(x=>{
      this.pricaPodaci=x;
    })
  }


  doniraj(){
    if(this.validacijaDonacije.valid)
    {
      let s  ={
        prica_id:this.storyID,
        korisnik_id:AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickinalog.id,
        ...this.validacijaDonacije.value
      }
      this.httpKlijent.post(Konfiguracija.adresaServera+'/Donacija/Add',s).subscribe(x=>{
        porukaSuccess('Donation successful');
        this.rut.navigateByUrl('/stories');
        this.httpKlijent.get(Konfiguracija.adresaServera+'/Donacija/GetUkupnoZaPricu/' +this.storyID).subscribe();
      })
    }
    else{
      porukaError("Donation is not confirmed. Please try again.");
    }


  }

  novaKartica() {
    this.validiraj=this.formBuilder.group({
      brojKartice:new FormControl('', [
        Validators.required,
        Validators.pattern('[0-9]*'),
        Validators.maxLength(6)
      ]),
      tipKartice:new FormControl('',[
        Validators.required
      ]),
      cvv:new FormControl('',[
        Validators.required,
        Validators.pattern('[0-9]*'),
        Validators.minLength(3),
        Validators.maxLength(3)
      ]),
      mjesecVazenja:new FormControl('',[
        Validators.required
      ]),
      godinaVazenja:new FormControl('', [
        Validators.required
      ])
    })
  }

  dodajKarticu() {
    if(this.validiraj.valid){
      let s={
        ...this.validiraj.value,
        id:0,
        korisnikID: AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickinalog.id
      };
      this.httpKlijent.post(Konfiguracija.adresaServera+"/Kartica/AddKarticu", s).subscribe(x=>{
        porukaSuccess("Successfully added new card.");
        this.kartica=null;
        this.rut.navigateByUrl('/donation');
      })
    }
    else{
      porukaError('Faild to add new card. Please try again.');
    }
  }

   countKartica() {
    this.httpKlijent.get(Konfiguracija.adresaServera+'/Kartica/CountKartica/'+AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickinalog.id).subscribe(x=>{
      this.broj=x;
    })
  }

  karticaPodaci() {
    this.httpKlijent.get(Konfiguracija.adresaServera+'/Kartica/GetKorisnikoveKartice/'+AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickinalog.id).subscribe(x=>{
      this.karticaPodatak=x;
    })
  }

  vratiNazad() {
      this.rut.navigate(['storyDetails',this.storyID]);
  }
}


