import React, {useEffect, useState} from "react";
import {useAccount, useMsal} from "@azure/msal-react";
import {useAppDispatch} from "../../redux/hooks";
//import {
//    addMessageToChannel, addMessageToUserChannel,
//    deleteMessageFromChannel, DeleteMessage
//} from "../../redux/features/servers/serverSlice";
import {createSignalRContext} from "react-signalr";
import {AccountInfo} from "@azure/msal-browser";
import {loginRequest} from "../../auth/authConfig";
import ServerComponent from "../Server";
import ChannelInfo from "../Channel/ChannelInfo";
import ChannelList from "../Channel/ChannelList";
import ChannelData from "../Channel/ChannelData";
import UserList from "../User/UserList";
import {useLocation} from "react-router-dom";

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

    /*
    SignalRContext.useSignalREffect(
        ChatCallbacksNames.DeleteMessageReceived,
        (channelId, messageId, serverId) => {
            let deleteMessage: DeleteMessage = {
                channelId: "",
                messageId: "",
                serverId: "",
            }

            if(channelId && messageId && serverId){
                deleteMessage = {
                    channelId: channelId,
                    messageId: messageId,
                    serverId: serverId
                }
            }
            dispatch(deleteMessageFromChannel(deleteMessage));
        }, []
    );
    */
    return (
        <>
            <ServerComponent/>
            <ChannelInfo />
            <ChannelList />
            <ChannelData />
            <UserList/>
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