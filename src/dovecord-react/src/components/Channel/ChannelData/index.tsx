import React, {useRef, useEffect, useState, MouseEvent, FormEvent} from "react";
import ChannelMessage, { Mention } from "../ChannelMessage";
import { Container, Messages, InputWrapper, Input, SendIcon } from "./styles";
import { useAppSelector } from "../../../redux/hooks";
import {
    MessageManipulationDto,
    useMessageGetMessagesFromChannelQuery,
    useMessageSaveMessageMutation,
} from "../../../redux/webApi";
import {getCurrentChannel} from "../../../redux/features/servers/serverSlice";
import {skipToken} from "@reduxjs/toolkit/query";

const ChannelData = () => {
    const messagesRef = useRef() as React.MutableRefObject<HTMLDivElement>;
    const [message, setMessage] = useState('');
    const currentChannel = useAppSelector(getCurrentChannel);
    const channelId = currentChannel?.id;
    const {data: messages, isLoading} = useMessageGetMessagesFromChannelQuery(channelId ? {id: channelId} : skipToken);
    const [sendMessage] = useMessageSaveMessageMutation();
    
    const Submit = async () => {
        const isMessageProvided = message && message !== '';

        if (isMessageProvided) {
            const newMessage = {
                channelId: currentChannel?.id,
                content: message,
                type: 0
            } as MessageManipulationDto;

            console.log(newMessage);
            await sendMessage({messageManipulationDto: newMessage})
            setMessage('');
        }
        else {
            alert('Message cannot be empty');
        }
    };

    const onMessageUpdate = (e: FormEvent<HTMLInputElement>): void => {
        e.preventDefault();
        setMessage(e.currentTarget.value);
    }

    useEffect(() => {
        const div = messagesRef.current;

        if (div) {
            div.scrollTop = div.scrollHeight;
        }
    }, [messagesRef]);

    /* MAP messages
    * Right now, just send "mention" as false, but can add ability later
    * */

    /*
                <ChannelMessage
                    author="a"
                    date="18/08/2020"
                    content={
                        <>
                            <Mention>@Gabriel Shelby</Mention>, terminando de
                            comer aqui, j?? vou
                        </>
                    }
                    hasMention
                    isBot
                />
     */
    return (
        <Container>
            <Messages ref={messagesRef}>
                {messages?.map((message) => (
                    <ChannelMessage
                        createdBy={message?.createdBy}
                        createdOn={message?.createdOn}
                        content={message?.content}
                        key={message?.id}
                        messageId={message.id}
                    />
                ))}
            </Messages>

            <InputWrapper>
                <Input
                    onChange={onMessageUpdate}
                    value={message}
                    type="text"
                    placeholder="Type your message..." />
                <SendIcon onClick={Submit} />
            </InputWrapper>
        </Container>
    );
};

export default ChannelData;

/*
            <Messages ref={messagesRef}>
                {currentChannel?.messages.map((message) => (
                    <ChannelMessage
                        author={message?.createdBy}
                        date={message?.createdOn}
                        content={message.content}
                        key={message.id}
                        messageId={message.id}
                    />
                ))}
            </Messages>
 */