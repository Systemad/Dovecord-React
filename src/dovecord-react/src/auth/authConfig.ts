import { LogLevel, Configuration, BrowserCacheLocation, RedirectRequest } from "@azure/msal-browser";

const isIE = window.navigator.userAgent.indexOf("MSIE ") > -1 || window.navigator.userAgent.indexOf("Trident/") > -1;

export const environment = {
    client: 'e6b54fdf-44c9-4ee1-b9a6-ce1c7f01bac9',
    authority: 'https://danovas.b2clogin.com/danovas.onmicrosoft.com/B2C_1_signupsignin1',
    authrityDomain: 'https://danovas.b2clogin.com/danovas.onmicrosoft.com/B2C_1_signupsignin1'
};

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

// Config object to be passed to Msal on creation
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

export const protectedResources = {
    weatherApi: {
        endpoint: "https://localhost:7045/weatherforecast",
        scopes: ["https://danovas.onmicrosoft.com/89be5e10-1770-45d7-813a-d47242ae2163/API.Access"],
    },
    channelApi: {
        endpoint: "https://localhost:7045/api/channels",
        scopes: ["https://danovas.onmicrosoft.com/89be5e10-1770-45d7-813a-d47242ae2163/API.Access"],
    },
    userApi: {
        endpoint: "https://localhost:7045/api/messages",
        scopes: ["https://danovas.onmicrosoft.com/89be5e10-1770-45d7-813a-d47242ae2163/API.Access"],
    },
    messageApi: {
        endpoint: "https://localhost:7045/api/users",
        scopes: ["https://danovas.onmicrosoft.com/89be5e10-1770-45d7-813a-d47242ae2163/API.Access"],
    },
    signalrhub:{
        endpoint: "https://localhost:7045/chathub",
        scopes: ["https://danovas.onmicrosoft.com/89be5e10-1770-45d7-813a-d47242ae2163/API.Access"]
    }
}

// Add here scopes for id token to be used at MS Identity Platform endpoints.
export const loginRequest: RedirectRequest = {
    scopes: ["https://danovas.onmicrosoft.com/89be5e10-1770-45d7-813a-d47242ae2163/API.Access"]
};
