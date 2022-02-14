import React from "react";
import { useEffect, useState } from "react";

import ChannelButton from "../ChannelButton";
import Button from '@mui/material/Button';
import { Container, Category, AddCategoryIcon } from "./styles";
import { getWeatherforecast, getChannels } from "../../services/services"

const ChannelList: React.FC = () => {

    async function loadWeather() {
        const response = await getChannels();
        console.log(response);
    }
    return (
        <Container>
            <Category>
                <span>Canais de texto</span>
                <AddCategoryIcon />
                <Button onClick={() => loadWeather()}>fetch</Button>
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
