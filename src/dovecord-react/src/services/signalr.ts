import * as signalR from "@microsoft/signalr";
import {PublicClientApplication} from "@azure/msal-browser";
import {loginRequest, msalConfig} from "../auth/authConfig";
import {ChannelMessageDto} from "./types";
import {useDispatch} from "react-redux";

/*
const connectionUrl = 'https://localhost:7045/chathub';


const msalInstance = new PublicClientApplication(msalConfig);

const acquireAccessToken = async (msalInstance: any) => {
    const activeAccount = msalInstance.getActiveAccount(); // This will only return a non-null value if you have logic somewhere else that calls the setActiveAccount API
    const accounts = msalInstance.getAllAccounts();
    if (!activeAccount && accounts.length === 0) {
        /*
        * User is not signed in. Throw error or wait for user to login.
        * Do not attempt to log a user in outside of the context of MsalProvider

    }
    const request = {
        scopes: loginRequest.scopes,
        account: activeAccount || accounts[0]
    };

    const authResult = await msalInstance.acquireTokenSilent(request);

    return authResult.accessToken
};

const options: signalR.IHttpConnectionOptions = {
    accessTokenFactory: async () => {
        const token = await acquireAccessToken(msalInstance);
        return token;
    }
}
const connection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl(connectionUrl, options)
    .build()


const setSignalrClientMethods = () => {

    connection.on("MessageReceived", (data: ChannelMessageDto) => {
        console.log("message received from Hub");
    });

    connection!.on("DeleteMessageReceived", (data: string) => {
        console.log("delete received from Hub");
        //this.updatedDataSelection(data, "delete");
    })

    connection!.on("UpdateData", (data) => {
        console.log("update from Hub");
        //this.updateDataTable();
    })
};

 */