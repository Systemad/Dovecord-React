import { Component, OnInit } from '@angular/core';
import { ChannelClient, ChannelDto, MessageClient, MessageManipulationDto, ChannelMessageDto } from '../web-api-client';
import { SignalRService } from '../services/signal-r.service';
@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.less']
})
export class ChatComponent implements OnInit {

  channels: ChannelDto[] = [];
  messages: ChannelMessageDto[] = [];

  selectedChannel: ChannelDto | undefined;
  messageToSend: MessageManipulationDto | undefined;

  constructor(private signalRService: SignalRService, private service: ChannelClient, private messageService: MessageClient) {
    this.service.getChannels().subscribe(result => {
      this.channels = result;
      //console.log(this.channels);
    }, error => console.error(error));

    this.signalRService.initiateSignalrConnection();
  }

  ngOnInit(): void {

    this.signalRService.data.subscribe((message: ChannelMessageDto) => {
      //let obj = new ChannelMessageDto(message);
      //console.log("chat component, message received", message);
      this.messages.push(message);
    });
  }


  selectChannel(channel: ChannelDto) {
    if(this.selectedChannel != null){
      this.signalRService.leaveChannel(String(this.selectedChannel.id));
    }
    this.selectedChannel = channel;

    this.signalRService.joinChannel(String(this.selectedChannel.id));
    this.getChannelMessages(channel);
  }

  getChannelMessages(channel: ChannelDto) : void
  {
    this.messageService.getMessagesFromChannel(channel.id!).subscribe(result => {
      this.messages = result;
      //console.log(result);
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
        //this.messages.push(result);
    }, error => console.log(error));
  }

  editMessage(message: ChannelMessageDto){
    this.messageService.updateMessage(message.id!, message.content).subscribe(result => {
        console.log(result);
    }, error => console.log(error));
  }

  deleteMessage(message: ChannelMessageDto){
    let idd = message.id;

    this.messages.forEach( (item, index) => {
      if(item === message) this.messages.splice(index,1);
    });

    this.messageService.deleteMessageById(message.id!).subscribe(
      results => {
        console.log(results);
      }, error => console.log(error));
  }
}
