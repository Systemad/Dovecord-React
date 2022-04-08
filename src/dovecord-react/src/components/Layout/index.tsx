import React, {useEffect, useState} from "react";
import {useAccount, useMsal} from "@azure/msal-react";
import {useAppDispatch} from "../../redux/hooks";
import {AccountInfo} from "@azure/msal-browser";
import {loginRequest} from "../../auth/authConfig";
import ServerComponent from "../Server";
import ChannelInfo from "../Channel/ChannelInfo";
import ChannelList from "../Channel/ChannelList";
import ChannelData from "../Channel/ChannelData";
import UserList from "../User/UserList";
import {useLocation} from "react-router-dom";
import connection from "../../redux/signalr";
import * as signalR from "@microsoft/signalr"
import UserComponent from "../User";
const Layout: React.FC = () => {
    const dispatch = useAppDispatch();
    const { instance, accounts, inProgress } = useMsal();
    const account = useAccount(accounts[0] || {});
    //const currentServer = useAppSelector(selectCurrentState).currentServer.

    const { pathname } = useLocation()

    const tokenRequest = {
        account: instance.getActiveAccount() as AccountInfo,
        scopes: loginRequest.scopes
    }

    const accessTokenFactory = async () => {
        const token = await instance.acquireTokenSilent(tokenRequest);
        return token.accessToken;
    }

    const startConnection = async () => {
        if(connection.state === signalR.HubConnectionState.Connected)
            return;

        if(connection.state === signalR.HubConnectionState.Disconnected)
            await connection.start();
    }
    useEffect(() => {
        startConnection().then(r => console.log(r));
    }, []);

    return (
        <>
            <ServerComponent/>
            <ChannelInfo />
            <ChannelList />
            <ChannelData />
            <UserComponent/>
        </>
    );
};
/*
                    <Grid>
                        <ServerList />
                        <ServerName />
                        <ChannelInfo />
                        <ChannelList />
                        <ChannelData />
                        <UserComponent />
                    </Grid>
 */
export default Layout;