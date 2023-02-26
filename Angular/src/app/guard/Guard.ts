import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from "@angular/router";
import {AutentifikacijaHelper} from "../helperi/autentifikacija-helper";
import {Injectable} from "@angular/core";
declare function porukaError(a: string):any;

@Injectable()
export class ProvjeraAktivacija implements CanActivate {
  constructor(private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot){
    if(AutentifikacijaHelper.getLoginInfo().isLogiran) {
          return true;
      }
    else {
      porukaError('Route not available, you have to log in first');
      this.router.navigateByUrl('logIn');
      return false;
    }
  }
}

