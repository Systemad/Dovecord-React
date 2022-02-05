import { Component, Input, OnInit } from '@angular/core';
import { UserDto } from '../web-api-client';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.less']
})
export class UserListComponent implements OnInit {

  @Input()
  users?: UserDto[];

  constructor() { }

  ngOnInit(): void {
  }

}
