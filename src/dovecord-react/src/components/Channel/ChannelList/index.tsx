import React, {useEffect, useState} from "react";
import ChannelButton from "../ChannelButton";
import { Container, Category, AddCategoryIcon } from "./styles";
import {useAppDispatch, useAppSelector} from "../../../redux/hooks";
import { Dialog, Group, Button, TextInput, Text } from '@mantine/core';
import {
    ChannelDto,
    ChannelManipulationDto,
    useServerAddServerChannelMutation,
    useServerGetChannelsQuery
} from "../../../redux/webApi";
import {
    getCurrentChannel,
    getCurrentServer,
    setCurrentChannel,
    setCurrentServer
} from "../../../redux/features/servers/serverSlice";
import {skipToken} from "@reduxjs/toolkit/query";

const ChannelList = () => {
    const currentServer = useAppSelector(getCurrentServer);
    const currentChannel = useAppSelector(getCurrentChannel);
    const dispatch = useAppDispatch();
    const serverId = currentServer?.id;

    const [addChannel] = useServerAddServerChannelMutation();
    const {data: channels} = useServerGetChannelsQuery(serverId ? {serverId: serverId} : skipToken);
    const [opened, setOpened] = useState(false);
    const [value, setValue] = useState('');

    const createChannel = () => {
        if(value !== null){
            const channelDto = {
                name: value,
                topic: "",
                type: 0,
            } as ChannelManipulationDto
            if(serverId !== null)
                addChannel({serverId: serverId, channelManipulationDto: channelDto})
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

            {channels?.map((channel) => (
                <ChannelButton
                    key={channel.id}
                    onClick={() => dispatch(setCurrentChannel(channel))}
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