import { Component } from '@angular/core';
import {LoginInformacije} from "../helperi/login-informacije";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {Konfiguracija} from "../../Config";
declare function porukaSuccess(a: string):any;
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent {

  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit()
  {

  }

  logOut() {
    AutentifikacijaHelper.setLoginInfo(null);

    this.httpKlijent.post(Konfiguracija.adresaServera + "/Autentifikacija/LogOut/", null)
      .subscribe((x: any) => {
        this.router.navigateByUrl("/");
        porukaSuccess("Logout successfull");
      });
  }


}
