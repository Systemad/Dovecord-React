import React from "react";

import {
    Container,
    HashtagIcon,
    Title,
    Separator,
    Description,
} from "./styles";
import {useAppSelector} from "../../../redux/hooks";
import {getCurrentChannel} from "../../../redux/features/servers/serverSlice";

const ChannelInfo = () => {
    const currentChannel = useAppSelector(getCurrentChannel);
    return (
        <Container>
            <HashtagIcon />
            <Title>{currentChannel?.name}</Title>
            <Separator />
            <Description>{currentChannel?.topic}</Description>
        </Container>
    );
};

export default ChannelInfo;
