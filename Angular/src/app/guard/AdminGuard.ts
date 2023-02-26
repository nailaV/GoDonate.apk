import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from "@angular/router";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";
import {Injectable} from "@angular/core";
declare function porukaError(a: string):any;

@Injectable()
export class AdminAktivacija implements CanActivate {
  constructor(private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot){
    if(AutentifikacijaHelper.getLoginInfo().isLogiran &&
      AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickinalog.isAdmin) {
      return true;
    }
    else {
      porukaError('Route not available, you dont have permissions to activate the route');
      this.router.navigateByUrl('logIn');
      AutentifikacijaHelper.setLoginInfo(null);
      return false;
    }
  }
}

