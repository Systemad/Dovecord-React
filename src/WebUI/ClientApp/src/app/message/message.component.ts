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
  styleUrls: ['./message.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
        provide: TUI_VALIDATION_ERRORS,
        useValue: {
            required: 'Message must be at least one character and up to 100! ',
            maxlength: maxLengthMessageFactory,
        },
    },
  ],
})


export class MessageComponent implements OnInit {

  readonly maxLength = 100;

  readonly messageForm = new FormGroup({
    textValue: new FormControl("", [
        Validators.required,
        Validators.maxLength(this.maxLength),
    ]),
  });

  @Input() messages: ChannelMessageDto[];
  @Output() sendMessageEmitter = new EventEmitter<string>();

  constructor() {
    this.messageForm.markAllAsTouched();
  }

  ngOnInit(): void {
  }

  sendMessage() {
    console.log(this.messageForm.get("textValue")?.value);
    this.sendMessageEmitter.emit(this.messageForm.get("textValue")?.value)
    this.messageForm.reset('');
  }
}
