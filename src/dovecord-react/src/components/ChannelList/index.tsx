import React, { useEffect, useState } from "react";
import ChannelButton from "../ChannelButton";
import Button from '@mui/material/Button';
import { Container, Category, AddCategoryIcon } from "./styles";
import {ChannelDto, ChannelMessageDto} from "../../services/types";
import {useDispatch, useSelector} from "react-redux";
import {selectChannels} from "../../redux/features/channels/channelSlice"
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import { setCurrentChannel } from "../../redux/uiSlice";

type ChannelState = {
    channel: ChannelDto
    messages: ChannelMessageDto[]
}

const ChannelList = () => {
    const dispatch = useAppDispatch();
    const channels = useAppSelector(selectChannels);

    return (
        <Container>
            <Category>
                <span>Text Channels</span>
                <AddCategoryIcon />
            </Category>

            {channels.map((channel) => (
                <ChannelButton
                    click={() => dispatch(setCurrentChannel(channel.channel))}
                    channel={channel.channel} />
            ))}
        </Container>
    );
};

export default ChannelList;