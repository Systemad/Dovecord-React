import React, {useEffect} from "react";

import ServerButton from "../ServerButton";

import {Container, Separator} from "./styles";
import {useAppDispatch, useAppSelector} from "../../../redux/hooks";
//import {
//    fetchChannelsAsync, fetchServersAsync,
//    selectCurrentState,
//    selectServers, ServerState,
//    setCurrentServer
//} from "../../../redux/features/servers/serverSlice";
import {useLocation, useNavigate} from "react-router-dom";
import {DiscoverButton} from "../../Discover/DiscoverButton/DsicoverButton";
import {useServerGetServersOfUserQuery} from "../../../redux/webApi";
import {ServerDto} from "../../../services/web-api-client";
import {setCurrentServer} from "../../../redux/features/servers/serverSlice";

const ServerList = () => {
        const dispatch = useAppDispatch()
        //const currentState = useAppSelector(selectCurrentState);
        const {data: servers, isLoading} = useServerGetServersOfUserQuery();
        //const currentServer = useAppSelector(selectCurrentState);
        //const servers = useAppSelector(selectServers);
        //const currentServer = useAppSelector(selectServers).find((server) => server.server.id === currentState.currentServer?.id);

        const navigate = useNavigate();
        const { pathname } = useLocation()
        const discoverActive = pathname.startsWith('/discover')

        const setServer = async (server?: string) => {
           //dispatch(setCurrentServer(server));
               //dispatch(fetchChannelsAsync(server.server.id!));
            if(server) {
                navigate(server);
            }
       }
    if(isLoading){
        return <p>loading</p>
    }

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
                    onClick={() => setServer(server.id)}
                    key={server.id}
                    selected={server.id === pathname}
                />
            ))};
        </Container>
    );
};

export default ServerList;

/*

                {servers?.map((server) => (
                    <ServerButton
                        onClick={() => setServer(server)}
                        key={server.server.id}
                        server={server.server}
                    />
 */
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
