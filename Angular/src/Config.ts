import {AutentifikacijaToken} from "./app/helperi/login-informacije";
import {AutentifikacijaHelper} from "./app/helperi/autentifikacija-helper";


export class Konfiguracija{
  static adresaServera ="https://localhost:7011";
  static http_opcije = function (){
    let autentifikacijaToken : AutentifikacijaToken = AutentifikacijaHelper.getLoginInfo().autentifikacijaToken;
    let mojToken = "";
    if(autentifikacijaToken!=null)
      mojToken=autentifikacijaToken.vrijednost;
    return{
      headers:{
        'autentifikacija-token':mojToken
      }
    }
  };
}
