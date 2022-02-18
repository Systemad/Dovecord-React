import React, {MouseEventHandler} from "react";

import { Container, HashtagIcon, InviteIcon, SettingsIcon } from "./styles";
import {ChannelDto} from "../../services/types";

export interface Props {
    channel: ChannelDto;
    click(channel: ChannelDto): void;
}

const ChannelButton: React.FC<Props> = ( {channel, click}) => {
    return (
        <Container className={"active"} onClick={() => click(channel)}>
            <div>
                <HashtagIcon />
                <span>{channel.name}</span>
            </div>
            <div>
                <InviteIcon />
                <SettingsIcon />
            </div>
        </Container>
    );
};

export default ChannelButton;
