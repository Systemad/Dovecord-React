/**
 * This file contains authentication parameters. Contents of this file
 * is roughly the same across other MSAL.js libraries. These parameters
 * are used to initialize Angular and MSAL Angular configurations in
 * in app.module.ts file.
 */

 import { LogLevel, Configuration, BrowserCacheLocation } from '@azure/msal-browser';

 import { environment } from 'src/environments/environment';

 const isIE = window.navigator.userAgent.indexOf("MSIE ") > -1 || window.navigator.userAgent.indexOf("Trident/") > -1;

 /**
  * Enter here the user flows and custom policies for your B2C application,
  * To learn more about user flows, visit https://docs.microsoft.com/en-us/azure/active-directory-b2c/user-flow-overview
  * To learn more about custom policies, visit https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-policy-overview
  */
 export const b2cPolicies = {
     names: {
         signUpSignIn: "B2C_1_signupsignin1"
     },
     authorities: {
         signUpSignIn: {
             authority: "https://danovas.b2clogin.com/danovas.onmicrosoft.com/B2C_1_signupsignin1",
         }
     },
     authorityDomain: "danovas.b2clogin.com"
 };

 /**
  * Configuration object to be passed to MSAL instance on creation.
  * For a full list of MSAL.js configuration parameters, visit:
  * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/configuration.md
  */
  export const msalConfig: Configuration = {
     auth: {
         clientId: environment.client, // This is the ONLY mandatory field that you need to supply.
         authority: b2cPolicies.authorities.signUpSignIn.authority, // Defaults to "https://login.microsoftonline.com/common"
         knownAuthorities: [b2cPolicies.authorityDomain], // Mark your B2C tenant's domain as trusted.
         redirectUri: '/', // Points to window.location.origin. You must register this URI on Azure portal/App Registration.
         postLogoutRedirectUri: '/', // Indicates the page to navigate after logout.
         navigateToLoginRequestUrl: true, // If "true", will navigate back to the original request location before processing the auth code response.
     },
     cache: {
         cacheLocation: BrowserCacheLocation.LocalStorage, // Configures cache location. "sessionStorage" is more secure, but "localStorage" gives you SSO between tabs.
         storeAuthStateInCookie: isIE, // Set this to "true" if you are having issues on IE11 or Edge
     },
     system: {
         loggerOptions: {
             loggerCallback(logLevel: LogLevel, message: string) {
                 console.log(message);
             },
             logLevel: LogLevel.Verbose,
             piiLoggingEnabled: false
         }
     }
 }

 /**
 * Add here the endpoints and scopes when obtaining an access token for protected web APIs. For more information, see:
 * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/resources-and-scopes.md
 */
export const protectedResources = {
  weatherApi: {
      endpoint: "https://localhost:7045/weatherforecast",
      scopes: ["https://danovas.onmicrosoft.com/89be5e10-1770-45d7-813a-d47242ae2163/API.Access"],
  },
}

export const loginRequest = {
  scopes: [...protectedResources.weatherApi.scopes] // If you would like the admin-user to explicitly consent via "Admin" page, instead of being prompted for admin consent during initial login, remove this scope.
};
