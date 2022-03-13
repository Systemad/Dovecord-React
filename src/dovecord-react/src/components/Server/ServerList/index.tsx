import React, {useEffect} from "react";

import ServerButton from "../ServerButton";

import { Container, Separator } from "./styles";
import {useAppDispatch, useAppSelector} from "../../../redux/hooks";
import {
        fetchChannelMessagesAsync, fetchChannelsAsync, fetchServersAsync,
        selectCurrentState,
        selectServers,
        setCurrentChannel, setCurrentServer
} from "../../../redux/features/servers/serverSlice";
import ChannelButton from "../../ChannelButton";
import {ServerDto} from "../../../services/types";

const ServerList: React.FC = () => {
        const dispatch = useAppDispatch()
        const currentState = useAppSelector(selectCurrentState);
        //const currentServer = useAppSelector(selectCurrentState);
        const servers = useAppSelector(selectServers);
        const currentServer = useAppSelector(selectServers).find((server) => server.server.id === currentState.currentServer?.id);

        const setServer = async (server: ServerDto) => {
                dispatch(setCurrentServer(server));
                if(currentServer?.loading === 'idle'){
                        dispatch(fetchChannelsAsync(server.id!));
                }
        }

        useEffect(() => {
                dispatch(fetchServersAsync());
        }, [])

        /*
                            <ChannelButton
                        click={() => setChannel(server)}
                        channel={channel.channel} />
                        onClick={() => setServer(server)}
         */
    return (
        <Container>

                {servers?.map((server) => (
                    <ServerButton
                        key={server.server.id}
                        server={server}
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
