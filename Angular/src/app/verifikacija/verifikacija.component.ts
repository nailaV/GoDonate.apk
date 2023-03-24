import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {Konfiguracija} from "../../Config";
declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;
@Component({
  selector: 'app-verifikacija',
  templateUrl: './verifikacija.component.html',
  styleUrls: ['./verifikacija.component.scss']
})
export class VerifikacijaComponent implements OnInit{
  verifikacijskiToken : string;
  korisnikID : any;
  constructor(private httpKlijent : HttpClient, private router : Router, private activated : ActivatedRoute) {
  }

  ngOnInit(): void {
    this.activated.params.subscribe(params=>{
      this.korisnikID=+params['korisnikID'];
    })
  }

  verifikuj() {
    let podaci={
      korisnikID:this.korisnikID,
      verifikacijskiToken:this.verifikacijskiToken
    }
    this.httpKlijent.post(Konfiguracija.adresaServera + '/Korisnik/Verifikuj',podaci).subscribe(response=>{
          if(response==true){
            porukaSuccess('Verification confirmed. Please log in');
            this.router.navigateByUrl('/logIn');
          }
          else
            porukaError('Verification code not matching. Please check again and enter valid code.')
    })
  }

  zatvoriFormu() {
    this.router.navigateByUrl('/registration');
  }
}
