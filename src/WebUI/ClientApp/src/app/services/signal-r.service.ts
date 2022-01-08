import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { MessagePackHubProtocol } from '@microsoft/signalr-protocol-msgpack'
import { BehaviorSubject } from 'rxjs';
import { ChannelMessageDto, IChannelMessageDto } from '../web-api-client';
import { protectedResources } from '../auth-config';
import { MsalService} from '@azure/msal-angular';
import { AuthService } from '../auth.service';

@Injectable({
  providedIn: 'root'
})

export class SignalRService {

  connectionUrl = 'https://localhost:7045/chathub';
  hubHelloMessage?: BehaviorSubject<string>;
  private dataSource = new BehaviorSubject<ChannelMessageDto>(new ChannelMessageDto());
  data = this.dataSource.asObservable();

  options: signalR.IHttpConnectionOptions = {
    accessTokenFactory: async () => {
      const x = await this.msalService.instance.acquireTokenSilent(protectedResources.signalrhub);
      return x.accessToken;
    }
  };

  connection?: signalR.HubConnection;

  constructor(
  private msalService: MsalService,
  private authService: AuthService) {
    this.authService.updateLoggedInStatus();
  }
  public initiateSignalrConnection(): Promise<void>{
    return new Promise((resolve, reject) => {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(this.connectionUrl, this.options)
        //.withHubProtocol(new MessagePackHubProtocol()) // Some reason doesn't work?
        .build();

      this.setSignalrClientMethods();

      this.connection
        .start()
        .then(() => {
          console.log(`signalr connection success! connectionId: ${this.connection!.connectionId}`);
          resolve();
      })
      .catch((error) => {
        console.log(`singalr connection error: ${error}`);
        reject();
      });
    });
  }

  private setSignalrClientMethods() : void {
    this.connection!.on("MessageReceived", (data: ChannelMessageDto) => {
      console.log("message received from Hub");
      this.updatedDataSelection(data);
    })
  }

  updatedDataSelection(data: any){
    this.dataSource.next(data);
  }

  public joinChannel(channelId: string){
    console.log("joining channel", channelId);
    this.connection?.invoke("JoinChannel", channelId);
  }

  public leaveChannel(channelId: string){
    console.log("leaving channel", channelId);
    this.connection?.invoke("LeaveChannel", channelId);
  }
}
