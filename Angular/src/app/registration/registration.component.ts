import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Konfiguracija} from "../../Config";
import {Router} from "@angular/router";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit{

  constructor(private httpKlijent: HttpClient,  private router : Router, private formBuilder:FormBuilder) {
    this.register=this.formBuilder.group({
      ime: new FormControl('', [
        Validators.required,
        Validators.minLength(2),
        Validators.pattern('[a-zA-Z ]*')]),
      prezime: new FormControl('', [
        Validators.required,
        Validators.minLength(2),
        Validators.pattern('[a-zA-Z ]*')]),
      datum_rodjenja:new FormControl('',[
        Validators.required]),
      email: new FormControl('', [
        Validators.required,
        Validators.email]),
      username: new FormControl('', [
        Validators.required,
        Validators.pattern("[a-zA-Z].*"),
        Validators.minLength(6),
        Validators.maxLength(12)]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(15)]),
      brojTelefona: new FormControl('',[
        Validators.required,
        Validators.pattern("[0-9]{3}/[0-9]{3}-[0-9]{3,4}")]),
      grad_id:new FormControl('',[
        Validators.required]),
      valuta_id:new FormControl('',[
        Validators.required
      ]),
      slikaKorisnika:new FormControl('',[
        Validators.required
      ])
    })
  }

  gradoviPodaci:any;
  register:FormGroup;
  valutaPodaci:any;
  korisnikID : any;

  get ime() : FormControl{
    return this.register.get("ime") as FormControl;
  }
  get prezime() : FormControl{
    return this.register.get("prezime") as FormControl;
  }
  get datum_rodjenja() : FormControl{
    return this.register.get("datum_rodjenja") as FormControl;
  }
  get email() : FormControl{
    return this.register.get("email") as FormControl;
  }
  get username() : FormControl{
    return this.register.get("username") as FormControl;
  }
  get password() : FormControl{
    return this.register.get("password") as FormControl;
  }
  get brojTelefona() : FormControl{
    return this.register.get("brojTelefona") as FormControl;
  }
  get slikaKorisnika() : FormControl{
    return this.register.get("slikaKorisnika") as FormControl;
  }
  get grad_id() : FormControl {
    return this.register.get("grad_id") as FormControl;
  }
  get valuta_id() : FormControl{
      return this.register.get("valuta_id") as FormControl;
    }



    ngOnInit(): void {

    this.preuzmiGradove();
    this.preuzmiValute();
  }

  preuzmiGradove() {
    this.httpKlijent.get(Konfiguracija.adresaServera + '/Grad/GetSviGradovi').subscribe(x => {
      this.gradoviPodaci= x;
    })
  }

  public slikab64:any;

  generisiPreview(event: any) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    let this2=this;
    reader.onload=()=>{
      this2.slikab64 = reader.result;
    };
  }

  RegisterDugme() {
    if(this.register.valid){
      let s={
        ...this.register.value,
        slikaKorisnika:this.slikab64
      };
      this.httpKlijent.post(Konfiguracija.adresaServera+'/Korisnik/Add',s).subscribe(x=>{
        this.korisnikID=x;
        porukaSuccess('Registration confirmed. Please log in.');
        this.router.navigate(['verifikacija',this.korisnikID]);
      })
    }
    else{
      porukaError('Registration not confirmed. Please try again.');
    }
  }

   preuzmiValute() {
        this.httpKlijent.get(Konfiguracija.adresaServera+'/Valuta/GetSveValute').subscribe(x=>{
          this.valutaPodaci=x;
        })
  }
}
