import React from "react";

import {
    Container,
    HashtagIcon,
    Title,
    Separator,
    Description,
} from "./styles";
import {selectChannel} from "../../app/features/channels/channelSlice"
import {useSelector} from "react-redux";

const ChannelInfo = ( /*props: {channel: ChannelDto} */) => {
    const currentChannel = useSelector(selectChannel);

    return (
        <Container>
            <HashtagIcon />
            <Title>{currentChannel.currentChannel.name}</Title>
            <Separator />
            <Description>{currentChannel.currentChannel.id}</Description>
        </Container>
    );
};

export default ChannelInfo;
