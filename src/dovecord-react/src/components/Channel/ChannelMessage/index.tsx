import React, {useState} from "react";

import { Container, Avatar, Message, Header, Content, OpenMenu, MenuContainer } from "./styles";
import { useHover } from '@mantine/hooks';
import { Popover, Menu, Divider, Text } from '@mantine/core';
export { Mention } from "./styles";

export interface Props {
    createdBy?: string | null;
    createdOn?: string;
    content?: string | React.ReactElement | React.ReactNode;
    hasMention?: boolean;
    isBot?: boolean;
    messageId: string;
}

const ChannelMessage: React.FC<Props> = ({
    createdBy,
    createdOn,
    content,
    hasMention,
    isBot,
    messageId
}) => {
    const { hovered, ref } = useHover();
    const [opened, setOpened] = useState(false);

    async function deleteMessage(){
        //if(messageId)
        //    await deleteV1MessagesId(messageId);
    }

    return (
        <Container className={hasMention ? "mention" : ""} ref={ref}>
            <Avatar className={isBot ? "bot" : ""} />

            <Message>
                <Header>
                    <strong>{createdBy}</strong>

                    {isBot && <span>Bot</span>}

                    <time>{createdOn}</time>
                    <Popover
                        opened={opened}
                        onClose={() => setOpened(false)}
                        noFocusTrap
                        noEscape
                        target={ hovered ?
                            <Menu trigger="hover" delay={500} position="right" withinPortal={false}>
                                <Menu.Item onClick={deleteMessage}>Delete</Menu.Item>
                                <Menu.Item >Edit</Menu.Item>
                                <Menu.Item >Copy</Menu.Item>
                            </Menu>
                            : ""}
                        width={50}
                        position="bottom"
                        placement="end"
                        >
                    </Popover>
                </Header>
                <Content>{content}</Content>
            </Message>
        </Container>
    );
};

export default ChannelMessage;
