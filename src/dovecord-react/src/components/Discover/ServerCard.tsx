import React from 'react';
import { Card, Image, Text, Badge, Button, Group, useMantineTheme } from '@mantine/core';
import {ServerDto} from "../../services/types";
import {postV1ServersJoinServerId} from "../../services/services";

interface Props {
    server?: ServerDto
}

export const ServerCard: React.FC<Props> = ({
    server
}) => {
    const theme = useMantineTheme();

    const joinServer = async (server?: ServerDto) => {
        if(server)
            await postV1ServersJoinServerId(server.id!)
    }
    const secondaryColor = theme.colorScheme === 'dark'
        ? theme.colors.dark[1]
        : theme.colors.gray[7];

    // <Image src="./image.png" height={160} alt="Norway" />
    return (
        <div style={{ width: 200, margin: 'auto' }}>
            <Card shadow="sm">
                <Card.Section>

                </Card.Section>

                <Group position="apart" style={{ marginBottom: 5, marginTop: theme.spacing.sm }}>
                    <Text weight={500}>{server?.name}</Text>
                </Group>

                <Text size="sm" style={{ color: secondaryColor, lineHeight: 1.5 }}>
                    INSERT SERVER DESCRIPTION
                </Text>

                <Button onClick={() => joinServer(server)} variant="light" color="blue" fullWidth style={{ marginTop: 14 }}>
                    Join Server
                </Button>
            </Card>
        </div>
    );
}