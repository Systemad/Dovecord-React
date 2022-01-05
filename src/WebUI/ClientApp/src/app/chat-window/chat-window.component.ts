import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { ChangeDetectionStrategy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';
import { ChannelMessageDto } from '../web-api-client';

@Component({
  selector: 'app-chat-window',
  templateUrl: './chat-window.component.html',
  styleUrls: ['./chat-window.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChatWindowComponent implements OnInit {

  @Input() messages?: ChannelMessageDto[];
  @Output() sendMessageEmitter = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  sendMessage(message: string) {
    this.sendMessageEmitter.emit(message);
  }
}
