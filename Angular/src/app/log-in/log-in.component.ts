import { Component } from '@angular/core';
import {FormControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {LoginInformacije} from "../helperi/login-informacije";
import {Konfiguracija} from "../../Config";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.scss']
})
export class LogInComponent {
  LogInForma:FormGroup;
  submitted=false;
  success=false;
  username:any;
  password:any;
  otvoriModal:boolean=false;
  unesiKod:boolean=false;
  promjena:boolean=false;
  mail:string;
  korisnikID:any;
  verifikacija:string;
  noviPassword:string;

  constructor(private formBuilder:FormBuilder, private router : Router, private httpKlijent:HttpClient) {

    this.LogInForma=this.formBuilder.group({
      username:new FormControl('',[ Validators.required, Validators.minLength(2)]),
      password:new FormControl('', [Validators.required, Validators.minLength(2)])
      })
  }

  onSubmit()
  {
    this.submitted=true;

    if(this.LogInForma.invalid)
    {
      return;
    }

    let saljemo = {
      username:this.username,
      password: this.password
    };
    this.httpKlijent.post<LoginInformacije>(Konfiguracija.adresaServera+ "/Autentifikacija/Login/", saljemo)
      .subscribe((x:LoginInformacije) =>{
        if (x.isLogiran) {
          porukaSuccess("Log in successfull!");
          AutentifikacijaHelper.setLoginInfo(x)
          this.router.navigateByUrl("/stories");
        }
        else
        {
          AutentifikacijaHelper.setLoginInfo(null);
         porukaError("Log in unsuccessful!");
        }
      });
  }

  ngOnInit()
  {

  }

  posaljiKod() {
    let s={
      mail:this.mail
    }
    this.httpKlijent.post(Konfiguracija.adresaServera+"/Korisnik/posaljiKod", s).subscribe(x=>{
      this.korisnikID=x;
      porukaSuccess("Code has been sent to an email.");
      this.otvoriModal=false;
      this.unesiKod=true;
    })
  }

  provjeriValidnost(){
    let s={
      korisnikID:this.korisnikID,
      verifikacijskiToken: this.verifikacija
    }
    this.httpKlijent.post(Konfiguracija.adresaServera+"/Korisnik/PorvjeriValidnost", s).subscribe(x=>{
      if(x==true){
        porukaSuccess("Code matched. Enter new password.");
        this.unesiKod=false;
        this.promjena=true;
      }
      else
        porukaError("Code didn't matched. Check again.")
    })
  }

  NoviPassword() {
    let s={
      id:this.korisnikID,
      noviPassword:this.noviPassword
    }
    this.httpKlijent.post(Konfiguracija.adresaServera+"/Korisnik/NoviPassword", s).subscribe(x=>{
      porukaSuccess("Password successfully changed. Now log in.");
      this.promjena=false;
      this.router.navigateByUrl('/logIn');
    })
  }
}
