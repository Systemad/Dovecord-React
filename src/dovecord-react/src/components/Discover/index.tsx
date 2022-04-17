import React, {useEffect, useState} from "react";
import {ServerCard} from "./ServerCard";
import { Container } from "./styles"
import {
    MessageManipulationDto,
    ServerDto, ServerManipulationDto, useServerAddServerMutation,
    useServerGetServersQuery, useServerJoinServerMutation
} from "../../redux/webApi";
import { Modal, Button, Group, TextInput, Checkbox, Box  } from '@mantine/core';
import { useForm } from '@mantine/form';

import connection from "../../redux/signalr";

export const DiscoverView: React.FC = () => {
    const [opened, setOpened] = useState(false);
    const {data: servers} = useServerGetServersQuery();
    const [addServer] = useServerJoinServerMutation();
    const [createServerMutation] = useServerAddServerMutation();

    const serverCreationForm = useForm({
        initialValues: {
            serverName: '',
        },

        validate: {
            serverName: (value) => (/\b\w{3,15}\b/.test(value) ? null : 'Name must be between 3..15 characters'),
        },
    });

    const joinServer = async (serverClicked: ServerDto) => {
        const server = serverClicked.id;
        if(server){
            addServer({serverId: server});
            await connection.invoke("JoinServer", server)
        }
    }
    const createServer = () => {
        const serverName = serverCreationForm.values.serverName;
        if(serverName){
            const newServer = {
                name: serverName
            } as ServerManipulationDto;
            createServerMutation({serverManipulationDto: newServer});
            window.location.reload();
            //const creationResponse = data;
            //if(creationResponse){
            //    window.location.reload();
                //await joinServer(creationResponse);
            //}
            setOpened(false);
        }
    }
    return (
        <>
            <Container>

                <h1>Servers</h1>

                <Group position="center">
                    <Button onClick={() => setOpened(true)}>Create server</Button>
                </Group>

                <Modal
                    opened={opened}
                    onClose={() => setOpened(false)}
                    title="Server creation"
                >
                    <Box sx={{ maxWidth: 300 }} mx="auto">
                        <form onSubmit={serverCreationForm.onSubmit((values) => console.log(values))}>
                            <TextInput
                                required
                                label="Server name"
                                placeholder="Enter server name"
                                {...serverCreationForm.getInputProps('serverName')}
                            />

                            <Group position="right" mt="md">
                                <Button onClick={() => createServer()} type="submit">Create</Button>
                            </Group>
                        </form>
                    </Box>
                </Modal>

                {servers?.map((server) => (
                    <ServerCard
                        key={server.id}
                        server={server}
                        onClick={() => joinServer(server)}>
                    </ServerCard>
                ))}
            </Container>
        </>
    )
};

/*
            <Container>
                {servers?.map((server) => (
                    <ServerCard
                        key={server.id}
                        server={server}
                        onClick={() => joinServer(server)}>
                    </ServerCard>
                ))}
            </Container>
 */