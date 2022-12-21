import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Konfiguracija} from "../../Config";
import {Router} from "@angular/router";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";

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
        Validators.pattern('[a-zA-Z]*'),]),
      prezime: new FormControl('', [
        Validators.required,
        Validators.minLength(2),
        Validators.pattern('[a-zA-Z]*'),]),
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
      slikaKorisnika:new FormControl('',[
        Validators.required]),
      grad_id:new FormControl('',[
        Validators.required])
    })
  }
  korisnik: any;
  gradoviPodaci:any;
  register:FormGroup;

  ngOnInit(): void {
    this.korisnik={
      id:0,
      ime:"",
      prezime:"",
      datum_rodjenja:"2022-12-18T20:23:57.848Z",
      email:"",
      username:"",
      password:"",
      brojTelefona:"",
      slikaKorisnika:"",
      grad_id:"",
      valuta_id:1
    }
    this.preuzmiGradove();

  }

  preuzmiGradove() {
    this.httpKlijent.get(Konfiguracija.adresaServera + '/Grad/GetSviGradovi').subscribe(x => {
      this.gradoviPodaci= x;
    })
  }

  generisiPreview() {
    // @ts-ignore
    var file = document.getElementById("formFile").files[0];
    if (file) {
      var reader = new FileReader();
      let this2 = this;
      reader.onload = function () {
        this2.korisnik.slikaKorisnika = reader.result.toString();
      }
      reader.readAsDataURL(file);
    }
  }

  RegisterDugme() {
    this.httpKlijent.post(`${Konfiguracija.adresaServera}/Korisnik/Add`, this.register.valid, Konfiguracija.http_opcije()).subscribe(x=>{
      this.router.navigateByUrl('/logIn');
    });
  }
}
