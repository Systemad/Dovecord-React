import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ChannelClient, ChannelDto, MessageClient, MessageManipulationDto, ChannelMessageDto } from '../web-api-client';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  channels: ChannelDto[];
  messages: ChannelMessageDto[];

  selectedChannel: ChannelDto | undefined;
  messageToSend: MessageManipulationDto | undefined;

  constructor(private service: ChannelClient, private messageService: MessageClient) {
    this.service.getChannels().subscribe(result => {
      this.channels = result;
      console.log(this.channels);
    }, error => console.error(error));
  }

  ngOnInit() {
  }


  selectChannel(channel: ChannelDto) {
    console.log(channel);
    this.selectedChannel = channel;
    this.getChannelMessages(channel);
  }

  getChannelMessages(channel: ChannelDto) : void
  {
    this.messageService.getMessagesFromChannel(channel.id!).subscribe(result => {
      this.messages = result;
      console.log(result);
    }, error => console.error(error));
  }

  sendMessage(message: string){
    let message2send = MessageManipulationDto.fromJS({
      content: message,
      channelId: this.selectedChannel?.id
    });


    this.messageService.saveMessage(message2send).subscribe(
      result => {
        console.log(result);
        this.messages.push(result);
    }, error => console.log(error));
  }
}
