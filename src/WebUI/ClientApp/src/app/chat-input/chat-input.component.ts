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
  styleUrls: ['./chat-input.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
        provide: TUI_VALIDATION_ERRORS,
        useValue: {
            required: 'Message must be at least between 1 and 100 characters',
            maxlength: maxLengthMessageFactory,
        },
    },
  ],
})
export class ChatInputComponent implements OnInit {

  readonly minLength = 1;
  readonly maxLength = 100;

  readonly messageForm = new FormGroup({
    textValue: new FormControl("", [
        Validators.required,
        Validators.minLength(this.minLength),
        Validators.maxLength(this.maxLength)
    ]),
  });

  constructor() {
    this.messageForm.markAllAsTouched();
  }

  @Output() sendMessageEmitter = new EventEmitter<string>();

  ngOnInit(): void {
  }

  sendMessage() {
    /*
    var text = new String(this.messageForm.get("textValue")?.value)
    console.log(this.messageForm.get("textValue")?.value);

    if(this.messageForm.value. > 1 && text.length < 100){
      this.sendMessageEmitter.emit(this.messageForm.get("textValue")?.value)
      this.messageForm.reset('');
    } else {
      console.log("do dialog");
    }
    */
    this.sendMessageEmitter.emit(this.messageForm.get("textValue")?.value)
    this.messageForm.reset('');
  }
}
