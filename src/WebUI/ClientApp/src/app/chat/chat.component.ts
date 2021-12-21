import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WeatherForecastClient, WeatherForecast } from '../web-api-client';
import { ChannelClient, ChannelDto, MessageClient, MessageManipulationDto } from '../web-api-client';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  public channels: ChannelDto[] = [];
  messages: MessageManipulationDto[] = [];

  selectedChannel: ChannelDto | undefined;

  constructor(private service: ChannelClient, private messageService: MessageClient) {
    this.service.getChannels().subscribe(result => {
      this.channels = result;
      console.log(this.channels);
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  selectChannel(channel: ChannelDto)
  {
    //console.log()
    this.selectedChannel = channel;
    //console.log(this.selectedChannel.temperatureC);
    this.getChannelMessages(channel);
  }

  getChannelMessages(channel: ChannelDto) : void
  {
    this.messageService.getMessagesFromChannel(channel.id!).subscribe(result => {
      this.messages = result;
      console.log(result);
    }, error => console.error(error));
  }
}
