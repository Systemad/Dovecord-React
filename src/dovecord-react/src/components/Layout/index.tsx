import React, {useEffect, useState} from "react";
import {HubConnection} from "@microsoft/signalr";
import {AuthenticatedTemplate, UnauthenticatedTemplate, useMsal} from "@azure/msal-react";
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
import {setChannels} from "../../redux/features/channels/channelSlice";
import {createSignalRContext} from "react-signalr";
import {Chat, ChatCallbacksNames} from "../../services/hub";
import {AccountInfo} from "@azure/msal-browser";
import {loginRequest} from "../../auth/authConfig";
import * as Console from "console";


type ChannelState = {
    channel: ChannelDto
    messages: ChannelMessageDto[]
}

type ChannelsState = {
    channels: ChannelState[],
}

const connectionUrl = 'https://localhost:7045/chathub';

const SignalRContext = createSignalRContext<Chat>();

const Layout: React.FC = () => {
    const dispatch = useAppDispatch();
    const [ connection, setConnection ] = useState<null | HubConnection>();
    const { instance, inProgress } = useMsal();

    const tokenRequest = {
        account: instance.getActiveAccount() as AccountInfo,
        scopes: loginRequest.scopes
    }

    const accessTokenFactory = async () => {
        const token = await instance.acquireTokenSilent(tokenRequest);
        console.log("LAYOUT: TOKEN FETCH FROM ACCESTOKEN FACTORY")
        console.log(token.accessToken);
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
        console.log("called count", count)
        dispatch(setChannels(newChannelData));
    }


    useEffect(() => {

        fetchChannels()
            .catch(console.error);

        console.log("LAYOUT USE FETCH")
    }, []);

    SignalRContext.useSignalREffect(
        ChatCallbacksNames.MessageReceived,
        (message) => {
            console.log(message);
        }, []
    );

    return (
        <>
            <AuthenticatedTemplate>
                <SignalRContext.Provider
                    connectEnabled={true}
                    accessTokenFactory={accessTokenFactory}
                    //dependencies={[]} //remove previous connection and create a new connection if changed
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
            </AuthenticatedTemplate>

            <UnauthenticatedTemplate>
                <SignInSignOutButton />
            </UnauthenticatedTemplate>
        </>
    );
};

export default Layout;
