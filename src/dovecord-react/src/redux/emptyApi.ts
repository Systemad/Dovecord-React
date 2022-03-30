// Or from '@reduxjs/toolkit/query' if not using the auto-generated hooks
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import {AxiosInstance} from "axios";
import {PublicClientApplication} from "@azure/msal-browser";
import {loginRequest, msalConfig} from "../auth/authConfig";

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

    console.log("msal acquireAccessToken: fetch token");
    const authResult = await msalInstance.acquireTokenSilent(request);

    //console.log(authResult.accessToken);
    return authResult.accessToken;
};

// initialize an empty api service that we'll inject endpoints into later as needed
export const emptySplitApi = createApi({
    reducerPath: 'api',
    baseQuery: fetchBaseQuery({
        baseUrl: 'https://localhost:7045',
        prepareHeaders: async (headers) => {
            const token = await acquireAccessToken(msalInstance);
            // If we have a token set in state, let's assume that we should be passing it.
            if (token) {
                headers.set('authorization', `Bearer ${token}`)
            }
            return headers
        },
    }),
    endpoints: () => ({}),
})