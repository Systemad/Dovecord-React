import React from "react";

import {
    Container,
    Profile,
    Avatar,
    UserData,
    Icons,
    MicIcon,
    HeadphoneIcon,
    SettingsIcon,
} from "./styles";
import {UserDto} from "../../../redux/webApi";

interface Props {
    user?: UserDto;
}

const UserInfo: React.FC<Props> = ({user}) => {
    return (
        <Container>
            <Profile>
                <Avatar />
                <UserData>
                    <strong>{user?.username}</strong>
                    <span>{user?.id[1]}</span>
                </UserData>
            </Profile>

            <Icons>
                <MicIcon />
                <HeadphoneIcon />
                <SettingsIcon />
            </Icons>
        </Container>
    );
};

export default UserInfo;
