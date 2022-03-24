import React from 'react';
import {SignInButton} from "../SignInButton";
import {Container, Box, Content} from "./styles"

export const LoginDisplay = () => {
    return (
        <>
            <Container>
                <Box>
                    <Content>
                        <SignInButton/>
                    </Content>
                </Box>
            </Container>
        </>
    )
};