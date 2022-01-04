import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { MessagePackHubProtocol } from '@microsoft/signalr-protocol-msgpack'
import { Observable, Subject } from 'rxjs';
import { MessageManipulationDto, ChannelMessageDto } from '../web-api-client';
import { protectedResources } from '../auth-config';
import { MsalService, MsalBroadcastService, MSAL_GUARD_CONFIG, MsalGuardConfiguration } from '@azure/msal-angular';
import { AccountInfo, AuthenticationResult, InteractionStatus, InteractionType, PopupRequest, RedirectRequest } from '@azure/msal-browser';
import { AuthService } from '../auth.service';

@Injectable({
  providedIn: 'root'
})

export class SignalRService {

  private connectionUrl = 'https://localhost:7045/chathub';

  options: signalR.IHttpConnectionOptions = {
    accessTokenFactory: async () => {
      const x = await this.msalService.instance.acquireTokenSilent(protectedResources.signalrhub);
      return x.accessToken;
    }
  };

  private hubConnection: signalR.HubConnection = new signalR.HubConnectionBuilder()
  .withUrl(this.connectionUrl, this.options)
  .withHubProtocol(new MessagePackHubProtocol())
  .build();

  private receivedMessageObject: ChannelMessageDto = new ChannelMessageDto();
  private sharedObj = new Subject<ChannelMessageDto>();

  constructor(
  private msalService: MsalService,
  private authService: AuthService) {

    // In order to succesfully grab token we need to update and set login status
    this.authService.updateLoggedInStatus();
    this.addListeners();
    this.hubConnection.start();
  }

  private addListeners(){
    this.hubConnection.on("MessageReceived", (data: ChannelMessageDto) => {
      console.log("message received from Hub");
      console.log(data);
      this.receivedMessageObject = data;
      this.sharedObj.next(this.receivedMessageObject);
    })
  }

  public retrieveMappedObject() : Observable<ChannelMessageDto> {
    return this.sharedObj.asObservable();
  }

  public joinChannel(channelId: string){
    console.log("joining channel", channelId);
    this.hubConnection.invoke("JoinChannel", channelId);
  }

  public leaveChannel(channelId: string){
    console.log("leaving channel", channelId);
    this.hubConnection.invoke("LeaveChannel", channelId);
  }
}
