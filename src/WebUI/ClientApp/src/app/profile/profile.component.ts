import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MsalService } from '@azure/msal-angular';

import { AuthService } from '../auth.service'

type ProfileType = {
  givenName?: string,
  surname?: string,
  userPrincipalName?: string,
  id?: string
};

let name: string;

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  constructor(
    private msal: MsalService
  ) { }

  ngOnInit(): void{
    console.log(this.getName());
  }

  getName() {
    return this.msal.instance.getActiveAccount();
  }
}
