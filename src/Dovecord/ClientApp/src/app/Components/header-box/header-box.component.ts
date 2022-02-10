import { Component, Input, OnInit } from '@angular/core';
import { ChannelDto } from '../web-api-client';

@Component({
  selector: 'app-header-box',
  templateUrl: './header-box.component.html',
  styleUrls: ['./header-box.component.css']
})
export class HeaderBoxComponent implements OnInit {

  @Input() currentChannel?: ChannelDto;

  constructor() { }

  ngOnInit(): void {
  }

}
