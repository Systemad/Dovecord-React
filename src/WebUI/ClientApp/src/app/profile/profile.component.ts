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

type AccountInfo = {
  homeAccountId?: string;
  environment?: string;
  tenantId?: string;
  username?: string;
  localAccountId?: string;
  name?: string;
  idTokenClaims?: object;
};

var name: string;

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profile: any;
  accountData: any;

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.profile = this.authService.getActiveAccount();
    console.log(this.profile)
  }
}
