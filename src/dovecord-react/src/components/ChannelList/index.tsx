import React, { useEffect, useState } from "react";
import ChannelButton from "../ChannelButton";
import Button from '@mui/material/Button';
import { Container, Category, AddCategoryIcon } from "./styles";
import {ChannelDto} from "../../services/types";
import {useDispatch} from "react-redux";
import store, { setChannel } from "../../store";

const ChannelList = (props: {channels: ChannelDto[] }) => {
    const dispatch = useDispatch();
    return (
        <Container>
            <Category>
                <span>Text Channels</span>
                <AddCategoryIcon />
            </Category>

            {props.channels.map((channel, key) => (
                <ChannelButton click={() => dispatch(setChannel(channel))} key={key} channel={channel} />
            ))}
        </Container>
    );
};

// LOOP ChannelButton
export default ChannelList;

/*

            {channels?.map((channel, index) => (
                <ChannelButton key={index} channel={channel} />
            ))}
 */