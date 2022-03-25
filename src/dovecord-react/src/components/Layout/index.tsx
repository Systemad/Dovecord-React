import React, {useEffect, useState} from "react";
import {AuthenticatedTemplate, UnauthenticatedTemplate, useAccount, useMsal} from "@azure/msal-react";
import {Grid} from "./styles";
import { Route, Routes} from 'react-router-dom';
import SignInSignOutButton from "../authentication/SignInSignOutButton";
import ServerList from "../Server/ServerList";
import ServerName from "../Server/ServerName";
import ChannelInfo from "../Channel/ChannelInfo";
import ChannelList from "../Channel/ChannelList";
import ChannelData from "../Channel/ChannelData";
import {ChannelDto, ChannelMessageDto} from "../../services/types";
import {useAppDispatch} from "../../redux/hooks";
import {
    addMessageToChannel, addMessageToUserChannel,
    deleteMessageFromChannel, DeleteMessage
} from "../../redux/features/servers/serverSlice";
import {createSignalRContext} from "react-signalr";
import {Chat, ChatCallbacksNames} from "../../services/hub";
import {AccountInfo} from "@azure/msal-browser";
import {loginRequest} from "../../auth/authConfig";
import {DiscoverView} from "../Discover";
import ServerComponent from "../Server";

const SignalRContext = createSignalRContext<Chat>();

const Layout: React.FC = () => {
    const dispatch = useAppDispatch();
    const { instance, accounts, inProgress } = useMsal();
    const account = useAccount(accounts[0] || {});
    //const currentServer = useAppSelector(selectCurrentState).currentServer.

    const tokenRequest = {
        account: instance.getActiveAccount() as AccountInfo,
        scopes: loginRequest.scopes
    }

    const accessTokenFactory = async () => {
        const token = await instance.acquireTokenSilent(tokenRequest);
        return token.accessToken;
    }

    SignalRContext.useSignalREffect(
        ChatCallbacksNames.MessageReceived,
        (message) => {
            if(message){
                console.log("message receive");
                if(message.type === 0){
                    dispatch(addMessageToChannel(message));
                } else if(message.type === 1) {
                    dispatch(addMessageToUserChannel(message))
                }
            }
        }, []
    );

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

    return (
        <>
            <SignalRContext.Provider
                connectEnabled={true}
                accessTokenFactory={accessTokenFactory}
                dependencies={[accessTokenFactory]} //remove previous connection and create a new connection if changed
                url={"https://dovecord1.azurewebsites.net/chathub"}>

                <Grid>
                    <ServerList />
                    <Routes>
                        <Route path="/chat" element={
                            <>
                                <ServerComponent/>
                                <ChannelInfo />
                                <ChannelList />
                                <ChannelData />
                            </>
                        }/>

                        <Route path="/discover" element={
                            <>
                                <DiscoverView/>
                            </>
                        }/>
                    </Routes>
                </Grid>
            </SignalRContext.Provider>
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
