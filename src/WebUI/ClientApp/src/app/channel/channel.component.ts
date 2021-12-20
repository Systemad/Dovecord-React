import { Component, TemplateRef } from '@angular/core';
import { ChannelClient, ChannelDto } from '../web-api-client';

@Component({
  selector: 'app-channel',
  templateUrl: './channel.component.html',
  styleUrls: ['./channel.component.css']
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

