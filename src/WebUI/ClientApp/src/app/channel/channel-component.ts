import { Component, TemplateRef } from '@angular/core';
import { ChannelClient, ChannelDto } from '../web-api-client';
;
@Component({
  selector: 'app-channel-component',
  templateUrl: './channel-component.html'
})
export class ChannelComponent {
  public channels: ChannelDto[] = [];

  constructor(client: ChannelClient){
    client.getChannels().subscribe(result => {
      this.channels = result;
      console.log(this.channels);
    }, error => console.log(error));
  }
}
