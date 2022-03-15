import React from "react";

import {
    Container,
    HashtagIcon,
    Title,
    Separator,
    Description,
} from "./styles";
import {useAppSelector} from "../../redux/hooks";
import {selectCurrentState} from "../../redux/features/servers/serverSlice";

const ChannelInfo = ( /*props: {channel: ChannelDto} */) => {
    const currentChannel = useAppSelector(selectCurrentState).currentChannel;
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
