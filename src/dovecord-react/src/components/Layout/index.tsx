import React from "react";

import { useState } from "react";
import { useMsal, useIsAuthenticated } from "@azure/msal-react";
import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";

import { loginRequest } from "../../auth/authConfig";

import Button from '@mui/material/Button';
import { Grid } from "./styles";

import SignInSignOutButton from "../authentication/SignInSignOutButton";
import ServerList from "../ServerList";
import ServerName from "../ServerName";
import ChannelInfo from "../ChannelInfo";
import ChannelList from "../ChannelList";
import UserInfo from "../UserInfo";
import UserList from "../UserList";
import ChannelData from "../ChannelData";

const Layout: React.FC = () => {
    const { instance } = useMsal();
    const handleLogin = () => {
        instance.loginRedirect(loginRequest);
    }

    return (
        <>
            <AuthenticatedTemplate>
                <Grid>
                    <ServerList />
                    <ServerName />
                    <ChannelInfo />
                    <ChannelList />
                    <UserInfo />
                    <ChannelData />
                    <UserList />
                </Grid>
            </AuthenticatedTemplate>

            <SignInSignOutButton />
        </>
    );
};

export default Layout;
