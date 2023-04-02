import {Component, OnDestroy, OnInit} from '@angular/core';
import {SignalRServis} from "../SignalR/SignalRService";

@Component({
  selector: 'app-notifikacije',
  templateUrl: './notifikacije.component.html',
  styleUrls: ['./notifikacije.component.scss']
})
export class NotifikacijeComponent implements OnInit{
  constructor(private signalRsERVIS : SignalRServis){
  }
  notifications: string[] = [];

  ngOnInit() {
      this.notifications  = this.signalRsERVIS.dohvatiObavijesti();
  }


}
