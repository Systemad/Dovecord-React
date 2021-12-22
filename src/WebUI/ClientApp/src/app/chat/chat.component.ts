import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WeatherForecastClient, WeatherForecast } from '../web-api-client';
import { ChannelClient, ChannelDto, MessageClient, MessageManipulationDto } from '../web-api-client';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  channels: ChannelDto[] = [];
  messages: MessageManipulationDto[] = [];

  selectedChannel: ChannelDto | undefined;
  messageToSend: MessageManipulationDto;
  inputField: string; // CONTTOL

  constructor(private service: ChannelClient, private messageService: MessageClient) {
    this.service.getChannels().subscribe(result => {
      this.channels = result;
      console.log(this.channels);
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  selectChannel(channel: ChannelDto) : void
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

  sendMessage(){


    console.log(this.inputField);
    this.messageToSend.channelId = "52f9764c-ab73-4b1a-8ac9-c6d6637d8e78"
    this.messageToSend.content = "hello"

    this.messageService.saveMessage(this.messageToSend);

    // RESET Input field
    this.inputField == "";
  }

}
