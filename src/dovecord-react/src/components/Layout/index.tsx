import React, {useEffect, useState} from "react";
import {HubConnection} from "@microsoft/signalr";
import {AuthenticatedTemplate, UnauthenticatedTemplate, useAccount, useMsal} from "@azure/msal-react";
import {Grid} from "./styles";

import {getChannels, getChannelsId, getMessagesChannelId} from "../../services/services"

import SignInSignOutButton from "../authentication/SignInSignOutButton";
import ServerList from "../Server/ServerList";
import ServerName from "../Server/ServerName";
import ChannelInfo from "../ChannelInfo";
import ChannelList from "../ChannelList";
import UserInfo from "../User/UserInfo";
import UserList from "../User/UserList";
import ChannelData from "../ChannelData";
import {ChannelDto, ChannelMessageDto} from "../../services/types";
import {useAppDispatch} from "../../redux/hooks";
import {
    addMessageToChannel,
    deleteMessageFromChannel,
    fetchChannelsAsync,
    setChannels
} from "../../redux/features/servers/serverSlice";
import {createSignalRContext} from "react-signalr";
import {Chat, ChatCallbacksNames} from "../../services/hub";
import {AccountInfo} from "@azure/msal-browser";
import {loginRequest} from "../../auth/authConfig";
import UserComponent from "../User";

type ChannelState = {
    channel: ChannelDto
    messages: ChannelMessageDto[]
}

type ChannelsState = {
    channels: ChannelState[],
}

type DeleteMessage = {
    channelId: string
    messageId: string
}

const SignalRContext = createSignalRContext<Chat>();

const Layout: React.FC = () => {
    const dispatch = useAppDispatch();
    const { instance, accounts, inProgress } = useMsal();
    const account = useAccount(accounts[0] || {});


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
            dispatch(addMessageToChannel(message!));
        }, []
    );

    SignalRContext.useSignalREffect(
        ChatCallbacksNames.DeleteMessageReceived,
        (channelId, messageId) => {
            let deleteMessage: DeleteMessage = {
                channelId: channelId!,
                messageId: messageId!
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
                    url={"https://localhost:7045/chathub"}
                >
                    <Grid>
                        <ServerList />
                        <ServerName />
                        <ChannelInfo />
                        <ChannelList />
                        <ChannelData />
                        <UserComponent />
                    </Grid>
                </SignalRContext.Provider>
            <UnauthenticatedTemplate>
                <SignInSignOutButton />
            </UnauthenticatedTemplate>
        </>
    );
};

export default Layout;
