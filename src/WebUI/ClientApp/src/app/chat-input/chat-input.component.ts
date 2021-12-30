import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { ChangeDetectionStrategy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';

export function maxLengthMessageFactory(context: {requiredLength: string}): string {
  return `Maximum length — ${context.requiredLength}`;
}

@Component({
  selector: 'app-chat-input',
  templateUrl: './chat-input.component.html',
  styleUrls: ['./chat-input.component.css'],
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
export class ChatInputComponent implements OnInit {

  readonly maxLength = 100;

  readonly messageForm = new FormGroup({
    textValue: new FormControl("", [
        Validators.required,
        Validators.maxLength(this.maxLength),
    ]),
  });

  constructor() {
    this.messageForm.markAllAsTouched();
  }

  @Output() sendMessageEmitter = new EventEmitter<string>();

  ngOnInit(): void {
  }

  sendMessage() {
    console.log(this.messageForm.get("textValue")?.value);
    this.sendMessageEmitter.emit(this.messageForm.get("textValue")?.value)
    this.messageForm.reset('');
  }
}
