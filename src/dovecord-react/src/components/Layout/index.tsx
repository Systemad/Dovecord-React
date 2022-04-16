import React, {useEffect, useState} from "react";
import {useAccount, useMsal} from "@azure/msal-react";
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import { useLocalStorageValue } from '@mantine/hooks';
import {AccountInfo} from "@azure/msal-browser";
import {loginRequest} from "../../auth/authConfig";
import ServerComponent from "../Server";
import ChannelInfo from "../Channel/ChannelInfo";
import ChannelList from "../Channel/ChannelList";
import ChannelData from "../Channel/ChannelData";
import {useLocation} from "react-router-dom";
import connection from "../../redux/signalr";
import * as signalR from "@microsoft/signalr"
import UserComponent from "../User";
import {getCurrentServer} from "../../redux/features/servers/serverSlice";
import Home from "../Home/index.ts";

const Layout: React.FC = () => {
    const dispatch = useAppDispatch();
    const { instance, accounts, inProgress } = useMsal();
    const account = useAccount(accounts[0] || {});
    const [value, setValue] = useLocalStorageValue({ key: 'color-scheme', defaultValue: 'dark' });
    const currentServer = useAppSelector(getCurrentServer);

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
            {currentServer == null ?
                <Home/> :
                <>
                <ServerComponent/>
                <ChannelInfo />
                <ChannelList />
                <ChannelData />
                <UserComponent/>
                </>
            }
        </>

    );
};

/*
            {isLoading ? (
                <>
                    <Skeleton height={48} width={48} circle mb="xl"/>
                    <Skeleton height={48} width={48} circle mb="xl"/>
                    <Skeleton height={48} width={48} circle mb="xl"/>
                </>
                ) : servers ? (
                    servers?.map((server) => (
                        <ServerButton
                            onClick={() => setServer(server)}
                            key={server.id}
                            selected={server.id === currentServer?.id}
                        />
                    ))
                ) : null }
 */
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