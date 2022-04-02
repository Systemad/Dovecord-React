import * as signalR from "@microsoft/signalr";
import {PublicClientApplication} from "@azure/msal-browser";
import {loginRequest, msalConfig} from "../auth/authConfig";

const url = "https://localhost:7045/chathub";
// TODO: Fix authentication
const msalInstance = new PublicClientApplication(msalConfig);

const acquireAccessToken = async (msalInstance: any) => {
    const activeAccount = msalInstance.getActiveAccount(); // This will only return a non-null value if you have logic somewhere else that calls the setActiveAccount API
    const accounts = msalInstance.getAllAccounts();
    if (!activeAccount && accounts.length === 0) {
        /*
         * User is not signed in. Throw error or wait for user to login.
         * Do not attempt to log a user in outside of the context of MsalProvider
         */
    }
    const request = {
        scopes: loginRequest.scopes,
        account: activeAccount || accounts[0],
    };

    console.log("signalr token fetch");
    const authResult = await msalInstance.acquireTokenSilent(request);

    //console.log(authResult.accessToken);
    return authResult.accessToken;
};

const options: signalR.IHttpConnectionOptions = {
    accessTokenFactory: async () => {
        const x = await acquireAccessToken(msalInstance);
        console.log(x.accessToken);
        return x.accessToken;
    }
};

export const connection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl(url /*, options*/)
    .build();