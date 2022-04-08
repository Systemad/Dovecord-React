import React, {useEffect} from "react";

import ServerButton from "../ServerButton";

import {Container, Separator} from "./styles";
import {useAppDispatch, useAppSelector} from "../../../redux/hooks";
import {useLocation, useNavigate} from "react-router-dom";
import {DiscoverButton} from "../../Discover/DiscoverButton/DsicoverButton";
import {ServerDto, useServerGetServersOfUserQuery, webApi} from "../../../redux/webApi";
import {getCurrentServer, setCurrentServer} from "../../../redux/features/servers/serverSlice";
import {Skeleton} from "@mantine/core";

const ServerList = () => {
        const dispatch = useAppDispatch()
        const {data: servers, isLoading} = useServerGetServersOfUserQuery();
        const currentServer = useAppSelector(getCurrentServer);
    /*
    const whatevs = dispatch(webApi.util.updateQueryData("serverGetServersOfUser", undefined, (draft) => {

            })
        )
     */
        const navigate = useNavigate();
        const { pathname } = useLocation()
        const discoverActive = pathname.startsWith('/discover')

        const setServer = async (server?: ServerDto) => {
            if(server){
                dispatch(setCurrentServer(server));
                //dispatch(setCurrentChannel({id: "0"}));
                navigate("/");
            }
       }

    return (
        <Container>
                <ServerButton isHome />
                <DiscoverButton
                    discoverActive={discoverActive}
                    to={"/discover"}>
                </DiscoverButton>
                <Separator />

            {isLoading ? (
                <>
                    <Skeleton height={48} width={48} circle mb="xl"/>
                    <Skeleton height={48} width={48} circle mb="xl"/>
                    <Skeleton height={48} width={48} circle mb="xl"/>
                </>
                ) : servers ? (
                    servers?.map((server) => (
                        <ServerButton
                            onClick={() => setServer(server)}
                            key={server.id}
                            selected={server.id === currentServer.id}
                        />
                    ))
                ) : null }
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
