import React, {useEffect, useState} from "react";
import {ServerCard} from "./ServerCard";
import { Container } from "./styles"
import {
    ServerDto,
    useServerGetServersQuery, useServerJoinServerMutation
} from "../../redux/webApi";
import connection from "../../redux/signalr";

export const DiscoverView: React.FC = () => {
    const {data: servers} = useServerGetServersQuery();
    const [addServer] = useServerJoinServerMutation();
    const joinServer = async (serverClicked: ServerDto) => {
        const server = serverClicked.id;
        if(server){
            addServer({serverId: server});
            await connection.invoke("JoinServer", server)
        }
    }
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