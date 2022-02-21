import React, { useEffect, useState } from "react";

import { useMsal, useIsAuthenticated } from "@azure/msal-react";
import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import { Grid } from "./styles";

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
import { setChannels } from "../../redux/features/channels/channelSlice";

type ChannelState = {
    channel: ChannelDto
    messages: ChannelMessageDto[]
}

type ChannelsState = {
    channels: ChannelState[],
}


const Layout: React.FC = () => {
    const dispatch = useAppDispatch();


    useEffect(() => {
        let isSubscribed = true
        const fetchChannels = async () => {
            if(isSubscribed){
                const channels = await getChannels();
                const data = channels.data;
                let newChannelData: ChannelsState = {
                    channels: []
                }

                for (let i = 0; i < channels.data.length; i++){
                    const channelDataFetch = await getChannelsId(data[i].id!);
                    const fetchChannelMessages = await getMessagesChannelId(data[i].id!);

                    const newChannel: ChannelState = {
                        channel: channelDataFetch.data,
                        messages: fetchChannelMessages.data
                    }
                    newChannelData.channels.push(newChannel);
                }
                dispatch(setChannels(newChannelData));
            }
        }

        fetchChannels()
            .catch(console.error);

        return () => { isSubscribed = false };
    }, []);

    return (
        <>
            <AuthenticatedTemplate>
                <Grid>
                    <ServerList />
                    <ServerName />
                    <ChannelInfo/>
                    <ChannelList/>
                    <UserInfo />
                    <ChannelData />
                    <UserList />
                </Grid>
            </AuthenticatedTemplate>

            <UnauthenticatedTemplate>
                <SignInSignOutButton />
            </UnauthenticatedTemplate>
        </>
    );
};

export default Layout;
