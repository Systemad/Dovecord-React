import React, { useEffect, useState } from "react";
import ChannelButton from "../ChannelButton";
import Button from '@mui/material/Button';
import { Container, Category, AddCategoryIcon } from "./styles";
import {ChannelDto} from "../../services/types";
import {useDispatch, useSelector} from "react-redux";
import {selectChannel, setChannel} from "../../app/features/channels/channelSlice"
const ChannelList = (props: {channels: ChannelDto[] }) => {
    const dispatch = useDispatch();
    const selectedChannel = useSelector(selectChannel);

    return (
        <Container>
            <Category>
                <span>Text Channels</span>
                <AddCategoryIcon />
            </Category>

            {props.channels.map((channel, key) => (
                <ChannelButton
                    selected={selectedChannel.currentChannel.id === channel.id}
                    click={() => dispatch(setChannel(channel))}
                    key={key}
                    channel={channel} />
            ))}
        </Container>
    );
};

export default ChannelList;