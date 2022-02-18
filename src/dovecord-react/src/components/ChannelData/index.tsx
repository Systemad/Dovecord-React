import React, {useRef, useEffect, useState, MouseEvent, FormEvent} from "react";

import ChannelMessage, { Mention } from "../ChannelMessage";

import { Container, Messages, InputWrapper, Input, InputIcon, SendIcon } from "./styles";
import {ChannelDto, ChannelMessageDto, MessageManipulationDto} from "../../services/types";
import {getMessagesChannelId, postMessages} from "../../services/services";
import {useDispatch, useSelector} from "react-redux";
import store, {selectChannel} from "../../store";

const ChannelData: React.FC = () => {
    const messagesRef = useRef() as React.MutableRefObject<HTMLDivElement>;
    const [message, setMessage] = useState('');
    const [messages, setMessages] = useState<ChannelMessageDto[]>([]);
    const currentChannelSelector = useSelector(selectChannel);

    /*
    const messagesFromChannel = async () =>{
        if(currentChannelSelector.currentChannel.id != null){
            const response = await getMessagesChannelId(currentChannelSelector.currentChannel.id!);
            setMessages(response.data);
        }
    }

     */
    const handleKeyPress = async (event: React.KeyboardEvent<HTMLInputElement>) => {
        event.preventDefault();
        if(event.key === "Enter"){
            const isMessageProvided = message && message !== '';

            if (isMessageProvided) {
                let newMessage =  {
                    channelId: currentChannelSelector.currentChannel.id!, // SET CURRENT CHANNEL
                    content: message,
                } as MessageManipulationDto;

                await postMessages(newMessage);
                setMessage('');
                //props.sendMessage(user, message);
            }
        }

    }

    const onSumbit = async () => {
        const isMessageProvided = message && message !== '';

        if (isMessageProvided) {
            let newMessage =  {
                channelId: currentChannelSelector.currentChannel.id,
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
    return (
        <Container>
            <Messages ref={messagesRef}>
                {Array.from(Array(15).keys()).map((n) => (
                    <ChannelMessage
                        author="Gabriel Shelby"
                        date="18/08/2020"
                        content="Bora jogar The Escapists"
                    />
                ))}

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
            </Messages>

            <InputWrapper>
                <Input
                    onChange={onMessageUpdate}
                    value={message}
                    type="text"
                    placeholder="Type your message..." />
                <SendIcon onClick={onSumbit} />
            </InputWrapper>
        </Container>
    );
};

export default ChannelData;
