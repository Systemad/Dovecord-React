import React, {useEffect, useState} from "react";
import {HubConnection} from "@microsoft/signalr";
import {AuthenticatedTemplate, UnauthenticatedTemplate, useAccount, useMsal} from "@azure/msal-react";
import {Grid} from "./styles";

import {getChannels, getChannelsId, getMessagesChannelId} from "../../services/services"

import SignInSignOutButton from "../authentication/SignInSignOutButton";
import ServerList from "../ServerList";
import ServerName from "../ServerName";
import ChannelInfo from "../ChannelInfo";
import ChannelList from "../ChannelList";
import UserInfo from "../UserInfo";
import UserList from "../UserList";
import ChannelData from "../ChannelData";
import {ChannelDto, ChannelMessageDto} from "../../services/types";
import {useAppDispatch} from "../../redux/hooks";
import {addMessageToChannel, setChannels} from "../../redux/features/channels/channelSlice";
import {createSignalRContext} from "react-signalr";
import {Chat, ChatCallbacksNames} from "../../services/hub";
import {AccountInfo} from "@azure/msal-browser";
import {loginRequest} from "../../auth/authConfig";

type ChannelState = {
    channel: ChannelDto
    messages: ChannelMessageDto[]
}

type ChannelsState = {
    channels: ChannelState[],
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

    const fetchChannels = async () => {
        const channels = await getChannels();
        const data = channels.data;
        let newChannelData: ChannelsState = {
            channels: []
        }
        let count = 0;
        for (let i = 0; i < data.length; i++){
            const channelDataFetch = await getChannelsId(data[i].id!);
            const fetchChannelMessages = await getMessagesChannelId(data[i].id!);

            const newChannel: ChannelState = {
                channel: channelDataFetch.data,
                messages: fetchChannelMessages.data
            }
            newChannelData.channels.push(newChannel);
            count++;
        }
        dispatch(setChannels(newChannelData));
    }


    useEffect(() => {

        fetchChannels()
            .catch(console.error);

    }, []);

    SignalRContext.useSignalREffect(
        ChatCallbacksNames.MessageReceived,
        (message) => {
            dispatch(addMessageToChannel(message!));
            console.log(message);
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
                        <ChannelInfo/>
                        <ChannelList/>
                        <UserInfo />
                        <ChannelData />
                        <UserList />
                    </Grid>
                </SignalRContext.Provider>
            <UnauthenticatedTemplate>
                <SignInSignOutButton />
            </UnauthenticatedTemplate>
        </>
    );
};

export default Layout;
