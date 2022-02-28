import React, {useRef, useEffect, useState, MouseEvent, FormEvent} from "react";

import ChannelMessage, { Mention } from "../ChannelMessage";

import { Container, Messages, InputWrapper, Input, SendIcon } from "./styles";
import { MessageManipulationDto } from "../../services/types";
import { postMessages } from "../../services/services";
import { useAppSelector } from "../../redux/hooks";
import { selectChannels } from "../../redux/features/channels/channelSlice"
import { getCurrentChannel } from "../../redux/uiSlice";

const ChannelData = () => {
    const messagesRef = useRef() as React.MutableRefObject<HTMLDivElement>;
    const [message, setMessage] = useState('');
    const currentRoom = useAppSelector(getCurrentChannel);
    const currentRoomData = useAppSelector(selectChannels);
    const messages = currentRoomData.find((room) => room.channel.id === currentRoom?.id);


    const handleKeyPress = async (event: React.KeyboardEvent<HTMLInputElement>) => {
        event.preventDefault();
        if(event.key === "Enter"){
            const isMessageProvided = message && message !== '';

            if (isMessageProvided) {
                let newMessage =  {
                    channelId: currentRoom?.id, // SET CURRENT CHANNEL
                    content: message,
                } as MessageManipulationDto;

                await postMessages(newMessage);
                setMessage('');
                //props.sendMessage(user, message);
            }
        }

    }

    const Submit = async () => {
        const isMessageProvided = message && message !== '';

        if (isMessageProvided) {
            let newMessage =  {
                channelId: currentRoom?.id,
                content: message,
            } as MessageManipulationDto;

            await postMessages(newMessage);
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
                {messages?.messages.map((message) => (
                    <ChannelMessage
                        author={message.createdBy!}
                        date={message.createdOn!}
                        content={message.content}
                        key={message.id}
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
