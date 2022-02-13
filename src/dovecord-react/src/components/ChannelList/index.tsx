import React from "react";

import ChannelButton from "../ChannelButton";

import { Container, Category, AddCategoryIcon } from "./styles";

const ChannelList: React.FC = () => {
    return (
        <Container>
            <Category>
                <span>Canais de texto</span>
                <AddCategoryIcon />
            </Category>

            <ChannelButton channelName="general-mourao" />
            <ChannelButton channelName="links" />
            <ChannelButton channelName="coding-notes" />
            <ChannelButton channelName="deep-web" />
            <ChannelButton channelName="random" />
        </Container>
    );
};

export default ChannelList;
