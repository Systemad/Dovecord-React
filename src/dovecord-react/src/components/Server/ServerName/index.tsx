import React from "react";

import { Container, Title, ExpandIcon } from "./styles";
import {useAppSelector} from "../../../redux/hooks";
import {useLocation, useParams} from "react-router-dom";
//import {selectCurrentState} from "../../../redux/features/servers/serverSlice";

const ServerName: React.FC = () => {
    const currentState = "test"; //useAppSelector(selectCurrentState).currentServer;
    const path = useLocation();
    return (
        <Container>
            <Title>{ currentState /*?.name*/ }</Title>

            <ExpandIcon />
        </Container>
    );
};

export default ServerName;
