import React, {useRef, useEffect, useState} from "react";

import ChannelMessage, { Mention } from "../ChannelMessage";

import { Container, Messages, InputWrapper, Input, InputIcon } from "./styles";
import {ChannelDto} from "../../services/types";
import {getMessagesChannelId} from "../../services/services";

const ChannelData: React.FC = () => {
    const messagesRef = useRef() as React.MutableRefObject<HTMLDivElement>;
    const [message, setMessage] = useState('');
    const [messages, setMessages] = useState([]);

    const messagesFromChannel = async (channel: ChannelDto) =>{
        const response = await getMessagesChannelId(channel.id);
        setMessages(response.data);
    }

    const onSumbit = (e) => {
        e.preventDefault();

        const isMessageProvided = message && message !== '';
        /*
        if (isUserProvided && isMessageProvided) {
            props.sendMessage(user, message);
        }
        else {
            alert('Please insert an user and a message.');
        }
         */
    };

    const onMessageUpdate = (e) => {
        setMessage(e.target.value);
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
                    author="Monteiro Shelby"
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
                <Input onChange={onMessageUpdate} value={message} type="text" placeholder="Conversar em #general-mourao" />
                <InputIcon />
            </InputWrapper>
        </Container>
    );
};

export default ChannelData;
