import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { ChangeDetectionStrategy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';
import { ChannelMessageDto } from '../web-api-client';


export function maxLengthMessageFactory(context: {requiredLength: string}): string {
  return `Maximum length — ${context.requiredLength}`;
}

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss']
})

export class MessageComponent implements OnInit {

  @Input() message?: ChannelMessageDto;

  constructor() {}

  ngOnInit(): void {
  }
}
