import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {Konfiguracija} from "../../Config";

@Component({
  selector: 'app-story-details',
  templateUrl: './story-details.component.html',
  styleUrls: ['./story-details.component.scss']
})
export class StoryDetailsComponent implements OnInit{
  pricaId : number;
  podaciPrica : any;

  constructor(private httpKlijent : HttpClient, private router : ActivatedRoute) {
  }


  ngOnInit() {
    this.router.params.subscribe(params=>{
      this.pricaId=+params['storyId'];
      this.fetchPricaById();
    })
  }

  private fetchPricaById() {
      this.httpKlijent.get(Konfiguracija.adresaServera + '/Prica/GetByPricaId/'+this.pricaId)
        .subscribe(x=>{
          this.podaciPrica=x;
        })
  }
  getSliku(x: number) {
    return `${Konfiguracija.adresaServera}/Prica/GetSlikaPrice/${x}`;
  }
}
