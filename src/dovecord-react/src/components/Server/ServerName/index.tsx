import React from "react";

import { Container, Title, ExpandIcon } from "./styles";
import {useAppSelector} from "../../../redux/hooks";
import {getCurrentServer} from "../../../redux/features/servers/serverSlice";

const ServerName: React.FC = () => {
    const currentServer = useAppSelector(getCurrentServer);
    return (
        <Container>
            <Title>{ currentServer?.name }</Title>

            <ExpandIcon />
        </Container>
    );
};

export default ServerName;
