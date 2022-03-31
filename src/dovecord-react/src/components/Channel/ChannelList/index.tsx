import React, {useEffect, useState} from "react";
import ChannelButton from "../ChannelButton";
import { Container, Category, AddCategoryIcon } from "./styles";
import {useAppDispatch, useAppSelector} from "../../../redux/hooks";
import { Dialog, Group, Button, TextInput, Text } from '@mantine/core';
import {ChannelDto, useServerGetChannelsQuery} from "../../../redux/webApi";
import {
    getCurrentChannel,
    getCurrentServer,
    setCurrentChannel,
    setCurrentServer
} from "../../../redux/features/servers/serverSlice";
import {skipToken} from "@reduxjs/toolkit/query";

interface Props {
    server?: string
}

const ChannelList = () => {
    const currentServer = useAppSelector(getCurrentServer);
    const currentChannel = useAppSelector(getCurrentChannel);
    const dispatch = useAppDispatch();
    const serverId = currentServer?.id;

    const {data: channels} = useServerGetChannelsQuery(serverId ? {serverId: serverId} : skipToken);
    const [opened, setOpened] = useState(false);
    const [value, setValue] = useState('');

    const setChannel = async (channel: ChannelDto) => {
        dispatch(setCurrentChannel(channel));
        //if(channel.loading === 'idle'){
        //    dispatch(fetchChannelMessagesAsync(channel.channel.id!));
        //}
    }

    /*
    const createChannel = async () => {
        if(value){
            const channelDto = {
                name: value,
                topic: "",
                type: 0,
            } as ChannelManipulationDto
            const newChannel = await postV1ServersServerIdChannels(currentServer!.server.id!, channelDto);
            if(newChannel){
                const newChannelState: ChannelState = {
                    channel: newChannel.data,
                    loading: 'idle',
                    messages: []
                }
                dispatch(addChannel(newChannelState));
            }
        }
        setOpened(false);
    }
    */
    return (
        <Container>
            <Category>
                <span>Text Channels</span>
                <AddCategoryIcon onClick={() => setOpened(true)} />
            </Category>

            <Dialog
                opened={opened}
                withCloseButton
                onClose={() => setOpened(false)}
                size="lg"
                radius="md"
            >
                <Text size="sm" style={{ marginBottom: 10 }} weight={500}>
                    Enter Channel name
                </Text>

                <Group align="flex-end">
                    <TextInput
                        variant="filled"
                        required
                        placeholder="Enter channel name"
                        style={{ flex: 1 }}
                        onChange={(event) => setValue(event.currentTarget.value)}
                    />
                    { /*<Button onClick={() => createChannel()}>Create Channel</Button>*/ }
                </Group>
            </Dialog>

            {channels?.map((channel) => (
                <ChannelButton
                    key={channel.id}
                    onClick={() => setChannel(channel)}
                    channel={channel}
                    selected={currentChannel?.id == channel.id}/>
            ))}

        </Container>
    );
};

export default ChannelList;

/*

            {currentServer?.channels.map((channel) => (
                <ChannelButton
                    key={channel.channel.id}
                    onClick={() => setChannel(channel)}
                    channel={channel.channel}
                    selected={currentState?.currentChannel?.id == channel.channel.id}/>
            ))}
 */