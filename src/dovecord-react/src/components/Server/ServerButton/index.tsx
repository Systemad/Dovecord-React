import React from "react";

import Logo from "../../../assets/Logo.svg";
import { Button } from "./styles";
import {ServerDto} from "../../../services/types";
import {useAppSelector} from "../../../redux/hooks";
import {selectCurrentState} from "../../../redux/features/servers/serverSlice";
import {useLocation} from "react-router-dom";

export interface Props {
    onClick?: (event: React.MouseEvent<HTMLButtonElement>) => void
    server?: ServerDto,
    isHome?: boolean;
    hasNotifications?: boolean;
    mentions?: number;
}

const ServerButton: React.FC<Props> = ({
    onClick,
    isHome,
    hasNotifications,
    mentions,
    server
}) => {
    const currentServer = useAppSelector(selectCurrentState).currentServer;
    const selected = server?.id === currentServer?.id;
    return (
        <Button
            onClick={onClick}
            isHome={isHome}
            hasNotifications={hasNotifications}
            mentions={mentions}
            className={selected ? "active" : ""}>
            {isHome && <img src={Logo} alt="Discord" />}
        </Button>
    );
};

export default ServerButton;
