import React, {useEffect, useState} from "react";
import {Container } from "./styles"
import {getV1Servers, postV1ServersJoinServerId} from "../../services/services";
import {ServerDto} from "../../services/types";
import {ServerCard} from "./ServerCard";
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {addServer, selectServers, ServerState} from "../../redux/features/servers/serverSlice";
export const DiscoverView: React.FC = () => {
    const [servers, setServers] = useState<ServerDto[]>([])
    const serversState = useAppSelector(selectServers);

    const dispatch = useAppDispatch();

    const joinServer = async (serverClicked?: ServerDto) => {
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
    }

    useEffect(() => {
        const getServers = async ()  => {
            const fetchServers = await getV1Servers();
            setServers(fetchServers.data);
        }
        getServers().then(r => console.log(r));
    }, [])

    return (
            <Container>
                {servers?.map((server) => (
                    <ServerCard
                        key={server.id}
                        server={server}
                        onClick={() => joinServer(server)}>
                    </ServerCard>
                ))}
            </Container>
    )
};