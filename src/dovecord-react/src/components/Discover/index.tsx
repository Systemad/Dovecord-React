import React, {useEffect, useState} from "react";
import {Container } from "./styles"
import {getV1Servers} from "../../services/services";
import {ServerDto} from "../../services/types";
import {ServerCard} from "./ServerCard";
export const DiscoverView: React.FC = () => {
    const [servers, setServers] = useState<ServerDto[]>([])


    useEffect(() => {
        const getServers = async ()  => {
            const fetchServers = await getV1Servers();
            setServers(fetchServers.data);
        }

        console.log("before");
        getServers().then(r => console.log(r));
        console.log("after");
    }, [])

    return (
            <Container>
                {servers?.map((server) => (
                    <ServerCard
                        key={server.id}
                        server={server}>
                    </ServerCard>
                ))}
            </Container>
    )
};