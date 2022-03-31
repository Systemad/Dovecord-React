import React, {useRef, useEffect, useState, MouseEvent, FormEvent} from "react";

import ChannelMessage, { Mention } from "../ChannelMessage";

import { Container, Messages, InputWrapper, Input, SendIcon } from "./styles";
import { useAppSelector } from "../../../redux/hooks";
import {
    MessageManipulationDto,
    useMessageGetMessagesFromChannelQuery,
    useServerGetChannelsQuery
} from "../../../redux/webApi";
import {getCurrentChannel} from "../../../redux/features/servers/serverSlice";
import {skipToken} from "@reduxjs/toolkit/query";
//import {selectCurrentState, selectServers} from "../../../redux/features/servers/serverSlice"

const ChannelData = () => {
    const messagesRef = useRef() as React.MutableRefObject<HTMLDivElement>;
    const [message, setMessage] = useState('');
    const currentChannel = useAppSelector(getCurrentChannel);
    const channelId = currentChannel?.id;
    const {data: messages, isLoading} = useMessageGetMessagesFromChannelQuery(channelId ? {id: currentChannel?.id} : skipToken);

    //const {data: channels} = useServerGetChannelsQuery(serverId ? {serverId: serverId} : skipToken);
    //const {data: channels} = useServerGetChannelsQuery({serverId: currentChannel.id ?? skipToken});
    //const currentServer = useAppSelector(selectCurrentState);
    //const currentServer = useAppSelector(selectServers).find((server) => server.server.id === currentState.currentServer?.id);
    //const currentChannel = currentServer?.channels.find((channel) => channel.channel.id === currentState.currentChannel?.id);

    const handleKeyPress = async (event: React.KeyboardEvent<HTMLInputElement>) => {
        event.preventDefault();
        if(event.key === "Enter"){
            const isMessageProvided = message && message !== '';

            if (isMessageProvided) {
                const newMessage =  {
                    //channelId: currentChannel!.channel.id, // SET CURRENT CHANNEL
                    content: message,
                } as MessageManipulationDto;

                await postMessage(newMessage);
                setMessage('');
                //props.sendMessage(user, message);
            }
        }

    }

    const Submit = () => {
        const isMessageProvided = message && message !== '';

        if (isMessageProvided) {
            const newMessage =  {
                //channelId: currentChannel!.channel.id, // SET CURRENT CHANNEL
                content: message,
                type: 0
            } as MessageManipulationDto;

            console.log(newMessage);
            //postV1Messages(newMessage); //.then(r => console.log(r));
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
                            comer aqui, já vou
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