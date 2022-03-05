import React, {MouseEventHandler} from "react";

import { Container, HashtagIcon, InviteIcon, SettingsIcon } from "./styles";
import {ChannelDto} from "../../services/types";
import {useParams} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../../redux/hooks";

import {getCurrentChannel} from "../../redux/uiSlice";

export interface Props {
    channel: ChannelDto;
    click(): void;
}

const ChannelButton: React.FC<Props> = ( {channel, click}) => {
    const currentChannel = useAppSelector(getCurrentChannel);
    const selected = channel.id === currentChannel?.id;

    return (
        <Container className={selected ? "active" : ""} onClick={() => click()}>
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
