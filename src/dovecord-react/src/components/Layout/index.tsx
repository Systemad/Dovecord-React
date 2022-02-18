import React, { useEffect, useState } from "react";

import { useMsal, useIsAuthenticated } from "@azure/msal-react";
import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import { Grid } from "./styles";

import {getChannels, getMessagesChannelId} from "../../services/services"

import SignInSignOutButton from "../authentication/SignInSignOutButton";
import ServerList from "../ServerList";
import ServerName from "../ServerName";
import ChannelInfo from "../ChannelInfo";
import ChannelList from "../ChannelList";
import UserInfo from "../UserInfo";
import UserList from "../UserList";
import ChannelData from "../ChannelData";
import {ChannelDto, ChannelMessageDto} from "../../services/types";
import channelData from "../ChannelData";

const Layout: React.FC = () => {

    const [channels, setChannels] = useState<ChannelDto[]>([]);
    const [messages, setMessages] = useState<ChannelMessageDto[]>([]);

    useEffect(() => {
        let isSubscribed = true;
        const fetchChannels = async () => {
            const response = await getChannels();
            const fetchedChannels = response.data;

            if(isSubscribed)
                setChannels(fetchedChannels);
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
                    <ChannelInfo /* channel={currentChannel} *//>
                    <ChannelList channels={channels} />
                    <UserInfo />
                    <ChannelData />
                    <UserList />
                </Grid>
            </AuthenticatedTemplate>

            <SignInSignOutButton />
        </>
    );
};

export default Layout;
