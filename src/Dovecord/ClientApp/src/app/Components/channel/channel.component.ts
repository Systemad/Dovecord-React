import { Component, TemplateRef, Input, EventEmitter, Output } from '@angular/core';
import { ChannelClient, ChannelDto } from '../web-api-client';

@Component({
  selector: 'app-channel',
  templateUrl: './channel.component.html',
  styleUrls: ['./channel.component.less']
})
export class ChannelComponent {

  @Input()
  channels?: ChannelDto[];

  @Output() channelEmitter = new EventEmitter<ChannelDto>();

  selectChannel(channel: ChannelDto){
    this.channelEmitter.emit(channel)
  }

  constructor(client: ChannelClient) {}
  /*
  {
    client.getChannels().subscribe(result => {
      this.channels = result;
      console.log(this.channels);
    }, error => console.log(error));
  }
  */
}

