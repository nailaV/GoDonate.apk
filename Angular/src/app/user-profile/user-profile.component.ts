import { Component } from '@angular/core';
import {Konfiguracija} from "../../Config";
import {LoginInformacije} from "../helperi/login-informacije";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent {


  getSlikuKorisnika(id:number) {
    return `${Konfiguracija.adresaServera}/Prica/GetSlikaPrice/${id}`;
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}
