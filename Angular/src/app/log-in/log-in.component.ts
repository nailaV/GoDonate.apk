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



  constructor(private formBuilder:FormBuilder, private router : Router, private httpKlijent:HttpClient) {

    this.LogInForma=this.formBuilder.group({
      username:new FormControl('', Validators.required),
      password:new FormControl('', Validators.required)
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
         porukaSuccess("Log in successful!");
          AutentifikacijaHelper.setLoginInfo(x)
          this.router.navigateByUrl("/stories");
        }
        else
        {
          AutentifikacijaHelper.setLoginInfo(null)
         porukaError("Log in unsuccessful!");
        }
      });
  }

  ngOnInit()
  {

  }

}
