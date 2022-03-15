import React from "react";

import { Container, Title, ExpandIcon } from "./styles";
import {useAppSelector} from "../../../redux/hooks";
import {selectCurrentState} from "../../../redux/features/servers/serverSlice";

const ServerName: React.FC = () => {
    const currentState = useAppSelector(selectCurrentState).currentServer;
    return (
        <Container>
            <Title>{ currentState?.name }</Title>

            <ExpandIcon />
        </Container>
    );
};

export default ServerName;
