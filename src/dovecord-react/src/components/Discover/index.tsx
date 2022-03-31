import React, {useEffect, useState} from "react";
import {ServerCard} from "./ServerCard";
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
//import {addServer, selectServers, ServerState} from "../../redux/features/servers/serverSlice";
import { Container } from "./styles"
import { useMantineTheme } from '@mantine/core';
import {ServerDto, useServerGetServersQuery} from "../../redux/webApi";

export const DiscoverView: React.FC = () => {
    //const [servers, setServers] = useState<ServerDto[]>([])
    //const serversState = useAppSelector(selectServers);

    const dispatch = useAppDispatch();

    const {data: servers} = useServerGetServersQuery();
    const joinServer = async (serverClicked?: ServerDto) => {
        /*
        if(serverClicked){
            const findServer = serversState.find((server) => server.server.id === serverClicked.id)
            if(!findServer){
                await postV1ServersJoinServerId(serverClicked.id!)
                const addNewServer: ServerState = {
                    channels: [],
                    loading: 'idle',
                    server: serverClicked,
                    users: []
                }
                dispatch(addServer(addNewServer));
            }
        }
         */
    }


    const theme = useMantineTheme();

    const secondaryColor = theme.colorScheme === 'dark'
        ? theme.colors.dark[1]
        : theme.colors.gray[7];

    return (
        <>
            <Container>
                    {servers?.map((server) => (
                        <ServerCard
                            key={server.id}
                            server={server}
                            onClick={() => joinServer(server)}>
                        </ServerCard>
                    ))}
            </Container>
        </>
    )
};

/*
            <Container>
                {servers?.map((server) => (
                    <ServerCard
                        key={server.id}
                        server={server}
                        onClick={() => joinServer(server)}>
                    </ServerCard>
                ))}
            </Container>
 */