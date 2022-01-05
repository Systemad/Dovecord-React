import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { MessagePackHubProtocol } from '@microsoft/signalr-protocol-msgpack'
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { MessageManipulationDto, ChannelMessageDto } from '../web-api-client';
import { protectedResources } from '../auth-config';
import { MsalService, MsalBroadcastService, MSAL_GUARD_CONFIG, MsalGuardConfiguration } from '@azure/msal-angular';
import { AccountInfo, AuthenticationResult, InteractionStatus, InteractionType, PopupRequest, RedirectRequest } from '@azure/msal-browser';
import { AuthService } from '../auth.service';

@Injectable({
  providedIn: 'root'
})

export class SignalRService {

  connectionUrl = 'https://localhost:7045/chathub';
  hubHelloMessage?: BehaviorSubject<string>;
  receivedMessage: BehaviorSubject<ChannelMessageDto | undefined>;

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

    // In order to succesfully grab token we need to update and set login status
    this.authService.updateLoggedInStatus();

    this.receivedMessage = new BehaviorSubject<ChannelMessageDto | undefined>(undefined);
  }
  public initiateSignalrConnection(): Promise<void>{
    return new Promise((resolve, reject) => {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(this.connectionUrl, this.options)
        .withHubProtocol(new MessagePackHubProtocol())
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
      //console.log(data);
      //this.receivedMessageObject = data;
      this.receivedMessage?.next(data);
    })
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
