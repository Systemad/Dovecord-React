import React, {useEffect, useState} from "react";
import {Tabs} from '@mantine/core';
import {Container} from "./styles"
const SettingsPage: React.FC = () => {

    const [activeTab, setActiveTab] = useState(1);

    /*
    useEffect(() => {

    });
     */

    return (
        <Container>
            <Tabs active={activeTab} onTabChange={setActiveTab}>
                <Tabs.Tab label="First">First tab content</Tabs.Tab>
                <Tabs.Tab label="Second">Second tab content</Tabs.Tab>
                <Tabs.Tab label="Third">Third tab content</Tabs.Tab>
                <Tabs.Tab label="Fourth">Third tab content</Tabs.Tab>
                <Tabs.Tab label="Fifth">Third tab content</Tabs.Tab>
                <Tabs.Tab label="Sixth">Third tab content</Tabs.Tab>
                <Tabs.Tab label="Seventh">Third tab content</Tabs.Tab>
                <Tabs.Tab label="Eight">Third tab content</Tabs.Tab>
                <Tabs.Tab label="Ninth">Third tab content</Tabs.Tab>
                <Tabs.Tab label="Tenths">Third tab content</Tabs.Tab>
            </Tabs>
        </Container>
    );
};

export default SettingsPage;