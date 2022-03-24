import React from 'react';
import { useState } from "react";
import { useMsal } from "@azure/msal-react";

import { loginRequest } from "../../auth/authConfig";
import Button from '@mui/material/Button';

export const SignInButton = () => {
    const { instance } = useMsal();

    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

    const handleLogin = (loginType: string) => {
        setAnchorEl(null);

        if (loginType === "popup") {
            instance.loginPopup(loginRequest);
        } else if (loginType === "redirect") {
            instance.loginRedirect(loginRequest);
        }
    }

    return (
        <div>
            <Button onClick={() => handleLogin("redirect")} key="loginRedirect">Sign in</Button>
        </div>
    )
};

// <Button onClick={() => handleLogin("popup")} key="loginPopup">Sign in using Popup</Button>