import React, {useEffect} from "react";
import ChannelButton from "../ChannelButton";
import { Container, Category, AddCategoryIcon } from "./styles";
import {ChannelDto, ChannelMessageDto} from "../../services/types";
import {
    fetchChannelMessagesAsync,
    fetchChannelsAsync, selectCurrentState, selectMainState, selectServers,
} from "../../redux/features/servers/serverSlice"
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import { setCurrentChannel } from "../../redux/features/servers/serverSlice";

type ChannelState = {
    channel: ChannelDto
    messages: ChannelMessageDto[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

const ChannelList = () => {
    const dispatch = useAppDispatch();
    const mainState = useAppSelector(selectMainState);
    const currentState = useAppSelector(selectCurrentState);
    const currentServer = useAppSelector(selectServers).find((server) => server.server.id === currentState.currentServer?.id);

    const setChannel = async (channel: ChannelState) => {
        dispatch(setCurrentChannel(channel.channel));
        if(channel.loading === 'idle'){
            dispatch(fetchChannelMessagesAsync(channel.channel.id!));
        }
    }
    useEffect(() => {
        if(currentServer){
            if(mainState.loading === 'idle'){
                dispatch(fetchChannelsAsync(currentServer!.server.id!));
            }
        }
    }, []);

    return (
        <Container>
            <Category>
                <span>Text Channels</span>
                <AddCategoryIcon />
            </Category>

            {currentServer?.channels.map((channel) => (
                <ChannelButton
                    key={channel.channel.id}
                    click={() => setChannel(channel)}
                    channel={channel.channel} />
            ))}
        </Container>
    );
};

export default ChannelList;