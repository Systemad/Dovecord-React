import React, {MouseEvent} from "react";

import { Container, HashtagIcon, InviteIcon, SettingsIcon } from "./styles";
import {ChannelDto} from "../../../services/types";

export interface Props {
    onClick?: (event: MouseEvent<HTMLButtonElement>) => void,
    channel?: ChannelDto,
    selected?: boolean
}

const ChannelButton = ({channel, onClick, selected}: Props) => {
    return (
        <Container className={selected ? "active" : ""}>
            <div>
                <HashtagIcon />
                <span onClick={onClick}>{channel?.name}</span>
            </div>
            <div>
                <InviteIcon />
                <SettingsIcon />
            </div>
        </Container>
    );
};

export default ChannelButton;
