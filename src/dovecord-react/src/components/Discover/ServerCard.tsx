import React from 'react';
import { Card, Image, Text, Badge, Button, Group, useMantineTheme } from '@mantine/core';
import {ServerDto} from "../../services/types";
import {postV1ServersJoinServerId} from "../../services/services";
import {useAppDispatch} from "../../redux/hooks";
import {addServer} from "../../redux/features/servers/serverSlice";

interface Props {
    server?: ServerDto
    onClick?: (event: React.MouseEvent<HTMLButtonElement>) => void
}

export const ServerCard: React.FC<Props> = ({
    server,
    onClick
}) => {
    const theme = useMantineTheme();
    const secondaryColor = theme.colorScheme === 'dark'
        ? theme.colors.dark[1]
        : theme.colors.gray[7];

    // <Image src="./image.png" height={160} alt="Norway" />
    return (
        <div style={{ width: 200, margin: 'auto' }}>
            <Card shadow="sm">
                <Card.Section>
                    <Image src="https://unsplash.it/800/600?image=11" height={160} alt="Norway" />
                </Card.Section>

                <Group position="apart" style={{ marginBottom: 5, marginTop: theme.spacing.sm }}>
                    <Text weight={500}>{server?.name}</Text>

                    <Badge color="pink" variant="light">
                        Already joined
                    </Badge>
                </Group>

                <Text size="sm" style={{ color: secondaryColor, lineHeight: 1.5 }}>
                    INSERT SERVER DESCRIPTION
                </Text>

                <Button onClick={onClick} variant="light" color="blue" fullWidth style={{ marginTop: 14 }}>
                    Join Server
                </Button>
            </Card>
        </div>
    );
}