import * as signalR from "@microsoft/signalr";
import {PublicClientApplication} from "@azure/msal-browser";
import {loginRequest, msalConfig} from "../auth/authConfig";

const url = "https://localhost:7045/chathub";
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

    return authResult.accessToken;
};

const connection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl(url, {accessTokenFactory: () => acquireAccessToken(msalInstance)})
    .build();

export default connection;