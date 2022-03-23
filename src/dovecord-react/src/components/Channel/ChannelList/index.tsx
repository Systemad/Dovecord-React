import React, {useEffect, useState} from "react";
import ChannelButton from "../ChannelButton";
import { Container, Category, AddCategoryIcon } from "./styles";
import {
    addChannel,
    ChannelState,
    fetchChannelMessagesAsync,
    selectCurrentState, selectServers,
} from "../../../redux/features/servers/serverSlice"
import {useAppDispatch, useAppSelector} from "../../../redux/hooks";
import { setCurrentChannel } from "../../../redux/features/servers/serverSlice";
import { Dialog, Group, Button, TextInput, Text } from '@mantine/core';
import {ChannelManipulationDto, MessageManipulationDto} from "../../../services/types";
import {postV1ServersServerIdChannels} from "../../../services/services";

const ChannelList = () => {
    const dispatch = useAppDispatch();
    const currentState = useAppSelector(selectCurrentState);
    const currentServer = useAppSelector(selectServers).find((server) => server.server.id === currentState.currentServer?.id);
    const [opened, setOpened] = useState(false);
    const [value, setValue] = useState('');

    const setChannel = async (channel: ChannelState) => {
        dispatch(setCurrentChannel(channel.channel));
        if(channel.loading === 'idle'){
            dispatch(fetchChannelMessagesAsync(channel.channel.id!));
        }
    }

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
                    <Button onClick={() => createChannel()}>Create Channel</Button>
                </Group>
            </Dialog>

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