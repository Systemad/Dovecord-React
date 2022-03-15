import React, {useEffect} from "react";

import ServerButton from "../ServerButton";

import {Container, Separator, SearchButton, SearchIcon} from "./styles";
import {useAppDispatch, useAppSelector} from "../../../redux/hooks";
import {
        fetchChannelMessagesAsync, fetchChannelsAsync, fetchServersAsync,
        selectCurrentState,
        selectServers,
        setCurrentChannel, setCurrentServer
} from "../../../redux/features/servers/serverSlice";
import ChannelButton from "../../ChannelButton";
import {ChannelDto, ChannelMessageDto, ServerDto, UserDto} from "../../../services/types";
import {useLocation} from "react-router-dom";
import {DiscoverButton} from "../../Discover/DiscoverButton/DsicoverButton";

type ServerState = {
        server: ServerDto;
        channels: ChannelState[]
        users: UserState[]
        loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

type ChannelState = {
        channel: ChannelDto
        messages: ChannelMessageDto[]
        loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

type UserState = {
        user: UserDto
        messages?: ChannelMessageDto[]
        loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

const ServerList: React.FC = () => {
        const dispatch = useAppDispatch()
        const currentState = useAppSelector(selectCurrentState);

        //const currentServer = useAppSelector(selectCurrentState);
        const servers = useAppSelector(selectServers);
        const currentServer = useAppSelector(selectServers).find((server) => server.server.id === currentState.currentServer?.id);

        const { pathname } = useLocation()
        const discoverActive = pathname.startsWith('/discover')

        const setServer = async (server: ServerState) => {
                dispatch(setCurrentServer(server.server));
                if(server.loading === 'idle'){
                        dispatch(fetchChannelsAsync(server.server.id!));
                }
        }

        /*
        useEffect(() => {
                dispatch(fetchServersAsync());
        }, [])

                            <ChannelButton
                        click={() => setChannel(server)}
                        channel={channel.channel} />
                        onClick={() => setServer(server)}
         */
    return (
        <Container>

                <ServerButton isHome />
                <DiscoverButton
                    discoverActive={discoverActive}
                    to={"/discover"}>
                </DiscoverButton>

                <Separator />

                {servers?.map((server) => (
                    <ServerButton
                        onClick={() => setServer(server)}
                        key={server.server.id}
                        server={server.server}
                    />

                ))}

        </Container>
    );
};

export default ServerList;

/*
            <ServerButton isHome />

            <Separator />

            <ServerButton />
            <ServerButton hasNotifications />
            <ServerButton mentions={3} />
            <ServerButton />
            <ServerButton />
            <ServerButton />
            <ServerButton hasNotifications />
            <ServerButton mentions={7} />
            <ServerButton />
            <ServerButton />
            <ServerButton hasNotifications />
 */
