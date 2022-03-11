import React from "react";

import Logo from "../../../assets/Logo.svg";
import { Button } from "./styles";
import {ServerDto} from "../../../services/types";
import {useAppSelector} from "../../../redux/hooks";
import {selectCurrentState} from "../../../redux/features/servers/serverSlice";

export interface Props {
    server: ServerDto,
    click(): void;
    isHome?: boolean;
    hasNotifications?: boolean;
    mentions?: number;
}

const ServerButton: React.FC<Props> = ({
    click,
    isHome,
    hasNotifications,
    mentions,
    server
}) => {

    const currentServer = useAppSelector(selectCurrentState);
    const selected = server.id === currentServer.currentServer?.id;

    return (
        <Button
            isHome={isHome}
            hasNotifications={hasNotifications}
            mentions={mentions}
            className={selected ? "active" : ""}
            click={() => click()}>
            {isHome && <img src={Logo} alt="Discord" />}
        </Button>
    );
};

export default ServerButton;
