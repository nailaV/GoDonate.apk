import {Injectable} from "@angular/core";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {Konfiguracija} from "../../Config";
declare function porukaSuccess(a: string):any;

@Injectable({
  providedIn:"root"
})
export class SignalRServis{
  private hubConnection : HubConnection;
  obavijesti :string[]=[];
  pokreniKonekciju(){
    this.hubConnection=new HubConnectionBuilder()
      .withUrl(Konfiguracija.adresaServera + '/notifikacije-putanja')
      .withAutomaticReconnect()
      .build();
    this.hubConnection.start()
      .then(() => {
        console.log('Konekcija zapoceta');
        this.hubConnection.on("PosaljiPoruke", (poruka: any) => {
          this.obavijesti = [];
          this.obavijesti.push(poruka);
          porukaSuccess('New story added, check the notifications panel for details');
        });
      })
      .catch(err => console.log('Problem sa konekcijom ' + err));

  }


  dohvatiObavijesti(){
    return this.obavijesti;
  }



}
