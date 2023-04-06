import { Component } from '@angular/core';
import {SignalRServis} from "./SignalR/SignalRService";
declare function porukaInfo(a: string):any;

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Angular';
  constructor(signalRservis : SignalRServis) {
    signalRservis.pokreniKonekciju();
  }
}
