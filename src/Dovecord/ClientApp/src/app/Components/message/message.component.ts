import { Component, OnInit, EventEmitter, Output, Input, ChangeDetectionStrategy, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';
import { ChannelMessageDto } from 'src/app/web-api-client';
import { TuiHostedDropdownComponent } from '@taiga-ui/core';
export function maxLengthMessageFactory(context: {requiredLength: string}): string {
  return `Maximum length — ${context.requiredLength}`;
}

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class MessageComponent implements OnInit {

  @ViewChild(TuiHostedDropdownComponent)
  component?: TuiHostedDropdownComponent;

  @Input() message?: ChannelMessageDto;
  @Output() deleteMessage: EventEmitter<string> = new EventEmitter();
  @Output() editMessage: EventEmitter<ChannelMessageDto> = new EventEmitter();

  constructor() {}

  ngOnInit(): void {
  }

  readonly items = ['Edit', 'Delete', 'Info'];

  open = false;

  onClick(item: string) {

    switch(item){
      case "Delete":{
        this.deleteMessage.emit(this.message?.id);
        break;
      }
      case "Edit":{
        this.editMessage.emit(this.message)
        //this.deleteMessage.emit(this.message);
        break;
      }
      case "Info":{
        console.log("info clicked");
        //this.deleteMessage.emit(this.message);
        break;
      }
    }

      this.open = false;

      if (this.component && this.component.nativeFocusableElement) {
          this.component.nativeFocusableElement.focus();
      }
  }
}
