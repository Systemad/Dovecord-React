import React from "react";

import {
    Container,
    HashtagIcon,
    Title,
    Separator,
    Description,
} from "./styles";
import {useSelector} from "react-redux";
import {useAppSelector} from "../../redux/hooks";
import {getCurrentChannel} from "../../redux/uiSlice";
//import {getCurrentChannel} from "../../redux/features/channels/channelSlice";

const ChannelInfo = ( /*props: {channel: ChannelDto} */) => {
    const currentChannel = useAppSelector(getCurrentChannel);
    return (
        <Container>
            <HashtagIcon />
            <Title>{currentChannel?.name}</Title>
            <Separator />
            <Description>{currentChannel?.id}</Description>
        </Container>
    );
};

export default ChannelInfo;
