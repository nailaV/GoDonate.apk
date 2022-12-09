import { Component } from '@angular/core';
import {LoginInformacije} from "../helperi/login-informacije";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent {

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit()
  {

  }
}
