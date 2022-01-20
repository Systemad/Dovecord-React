import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { MessagePackHubProtocol } from '@microsoft/signalr-protocol-msgpack'
import { BehaviorSubject } from 'rxjs';
import { ChannelMessageDto, IChannelMessageDto } from '../web-api-client';
import { protectedResources } from '../auth/auth-config';
import { MsalService} from '@azure/msal-angular';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})

export class SignalRService {

  connectionUrl = 'https://localhost:7045/chathub';
  private messageReceivedSource = new BehaviorSubject<ChannelMessageDto>(new ChannelMessageDto());
  private deleteMessageReceivedSource = new BehaviorSubject<any>(null);

  messageReceivedObservable = this.messageReceivedSource.asObservable();
  deleteMessageReceivedObservable = this.deleteMessageReceivedSource.asObservable();

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
  public initiateSignalrConnection(): Promise<any>{
    return new Promise((resolve, reject) => {
      this.connection = new signalR.HubConnectionBuilder()
        .withAutomaticReconnect()
        .withUrl(this.connectionUrl, this.options)
        // Since there are resolver issues from PascalCase (C# class) to CamelCase Typescript
        // and no real good solution, we will be leaving this out for now
        //.withHubProtocol(new MessagePackHubProtocol())
        .build();

      this.setSignalrClientMethods();

      this.connection
        .start()
        .then(() => {
          console.log(`signalr connection success! connectionId: ${this.connection!.connectionId}`);
          resolve(true);
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
        this.updatedDataSelection(data, "add");
    })

    this.connection!.on("DeleteMessageReceived", (data: string) => {
      console.log("delete received from Hub");
      this.updatedDataSelection(data, "delete");
    })
  }

  private checkConnectedState(): boolean {
    if(this.connection && this.connection.state == signalR.HubConnectionState.Connected){
      return true;
    }
    return false;
  }

  private updatedDataSelection(data: any, tag: string){
    switch(tag){
      case "add": {
        this.messageReceivedSource.next(data);
        break;
      };
      case "delete": {
        this.deleteMessageReceivedSource.next(data);
        break;
      }
    }
  }

  public joinChannel(channelId: string) : void {
    if(this.checkConnectedState()){
      console.log("joining channel", channelId);
      this.connection?.invoke("JoinChannel", channelId);
    }
  }

  public leaveChannel(channelId: string) : void {
    if(this.checkConnectedState()){
      console.log("leaving channel", channelId);
      this.connection?.invoke("LeaveChannel", channelId);
    }
  }
}
