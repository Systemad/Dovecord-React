import React from "react";

import { Container, HashtagIcon, InviteIcon, SettingsIcon } from "./styles";
import {ChannelDto} from "../../services/types";

const ChannelButton = ( props: {channel: ChannelDto} ) => {
    return (
        <Container className={"active"}>
            <div>
                <HashtagIcon />
                <span>{props.channel.name}</span>
            </div>
            <div>
                <InviteIcon />
                <SettingsIcon />
            </div>
        </Container>
    );
};

export default ChannelButton;
