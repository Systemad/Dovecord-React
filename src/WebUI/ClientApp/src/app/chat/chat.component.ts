import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ChannelClient, ChannelDto, MessageClient, MessageManipulationDto, ChannelMessageDto } from '../web-api-client';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  channels: ChannelDto[] = [];
  messages: ChannelMessageDto[] = [];

  selectedChannel: ChannelDto | undefined;
  messageToSend: MessageManipulationDto | undefined;
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

    let itme = MessageManipulationDto.fromJS({
      content: "ahahah",
      channelId: "bd80695d-645b-cf0f-a077-3374e30e4ec9"
    });

    //console.log(this.inputField);
    //this.messageToSend!.channelId = "9efa93cb-13e2-91b5-8e1d-a5dae450377e";
    //this.messageToSend.content = "hello"

    this.messageService.saveMessage(itme).subscribe(
      result => {
      this.messages.push(result);
      console.log(result);
    }, error => console.log(error));

    // RESET Input field
    this.inputField == "";
  }

}
