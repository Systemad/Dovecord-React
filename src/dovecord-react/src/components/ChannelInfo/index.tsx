import React from "react";

import {
    Container,
    HashtagIcon,
    Title,
    Separator,
    Description,
} from "./styles";
import {ChannelDto} from "../../services/types";

const ChannelInfo = ( /*props: {channel: ChannelDto} */) => {
    return (
        <Container>
            <HashtagIcon />
            <Title>props.channel.name</Title>
            <Separator />
            <Description>TODO: props.channel.name</Description>
        </Container>
    );
};

export default ChannelInfo;
