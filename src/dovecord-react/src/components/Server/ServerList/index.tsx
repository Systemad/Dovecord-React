import React, {useEffect} from "react";

import ServerButton from "../ServerButton";

import {Container, Separator} from "./styles";
import {useAppDispatch, useAppSelector} from "../../../redux/hooks";
import {
    fetchChannelsAsync, fetchServersAsync,
    selectCurrentState,
    selectServers, ServerState,
    setCurrentServer
} from "../../../redux/features/servers/serverSlice";
import {useLocation, useNavigate} from "react-router-dom";
import {DiscoverButton} from "../../Discover/DiscoverButton/DsicoverButton";

const ServerList: React.FC = () => {
        const dispatch = useAppDispatch()
        const currentState = useAppSelector(selectCurrentState);

        //const currentServer = useAppSelector(selectCurrentState);
        const servers = useAppSelector(selectServers);
        const currentServer = useAppSelector(selectServers).find((server) => server.server.id === currentState.currentServer?.id);

        const { pathname } = useLocation()
        const discoverActive = pathname.startsWith('/discover')
        const navigate = useNavigate();
        const setServer = async (server: ServerState) => {
            dispatch(setCurrentServer(server.server));
            if(server.loading === 'idle'){
                dispatch(fetchChannelsAsync(server.server.id!));
            }
            navigate("/chat");
        }
        useEffect(() => {
            dispatch(fetchServersAsync());
        }, [])
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
