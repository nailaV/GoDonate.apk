import {LoginInformacije} from "./login-informacije";

export class AutentifikacijaHelper{
  static setLoginInfo(x:LoginInformacije) : void {
    if (x == null)
      x = new LoginInformacije();
    localStorage.setItem("autentifikacija-token", JSON.stringify(x));
  }

  static getLoginInfo() : LoginInformacije{
    let x = localStorage.getItem("autentifikacija-token");
    if(x=="")
      return new LoginInformacije();
    try {
        let loginInfo : LoginInformacije = JSON.parse(x);
        if(loginInfo==null)
          return new LoginInformacije();
        return loginInfo;
    }
    catch (e)
    {
        return new LoginInformacije();
    }
  }

}
