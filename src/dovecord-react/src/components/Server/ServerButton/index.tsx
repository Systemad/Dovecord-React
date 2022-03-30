import React from "react";

import Logo from "../../../assets/Logo.svg";
import { Button, Pill } from "./styles";
import {useAppSelector} from "../../../redux/hooks";
import {ServerDto} from "../../../services/web-api-client";
//import {selectCurrentState} from "../../../redux/features/servers/serverSlice";

export interface Props {
    onClick?: (event: React.MouseEvent<HTMLButtonElement>) => void
    selected?: boolean,
    isHome?: boolean;
    hasNotifications?: boolean;
    mentions?: number;
}

const ServerButton: React.FC<Props> = ({
    onClick,
    isHome,
    hasNotifications,
    mentions,
    selected
}) => {
    return (
        <>
            <Pill/>
            <Button
                onClick={onClick}
                isHome={isHome}
                hasNotifications={hasNotifications}
                mentions={mentions}
                className={selected ? "active" : ""}>
                {isHome && <img src={Logo} alt="Discord" />}
            </Button>
        </>
    );
};

export default ServerButton;
