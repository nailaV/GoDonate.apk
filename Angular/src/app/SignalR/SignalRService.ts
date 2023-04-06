import {Injectable} from "@angular/core";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {Konfiguracija} from "../../Config";
declare function porukaSuccess(a: string):any;
declare function porukaInfo(a: string):any;

@Injectable({
  providedIn:"root"
})
export class SignalRServis{
  private hubConnection : HubConnection;
  pokreniKonekciju(){
    this.hubConnection=new HubConnectionBuilder()
      .withUrl(Konfiguracija.adresaServera + '/notifikacije-putanja')
      .withAutomaticReconnect()
      .build();
    this.hubConnection.start()
      .then(() => {
        console.log('Konekcija zapoceta');
        this.hubConnection.on("PosaljiPoruke", (poruka: string) => {
          porukaInfo(`${poruka}`)
        });
      })
      .catch(err => console.log('Problem sa konekcijom ' + err));
  }

}
