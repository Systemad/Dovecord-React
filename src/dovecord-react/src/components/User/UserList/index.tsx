import React from "react";

import { Container, Role, User, Avatar } from "./styles";
import {UserDto} from "../../../services/types";

interface UserProps {
    user: UserDto;
    isBot?: boolean;
}

// Set avatar badge like avatar but for online status
const UserRow: React.FC<UserProps> = ({ user, isBot }) => {
    return (
        <User>
            <Avatar className={isBot ? "bot" : ""} />

            <strong>{user.name}</strong>

            {isBot && <span>Bot</span>}
        </User>
    );
};

interface UserListProps {
    onlineUsers: UserDto[];
    offlineUsers: UserDto[];
}

const UserList: React.FC<UserListProps> = ({onlineUsers, offlineUsers}) => {
    return (
        <Container>
            <Role>Online - {onlineUsers.length}</Role>
            {onlineUsers.map((user) => (
                <UserRow
                    user={user}
                    isBot={false}/>
            ))}

            <Role>Offline - {offlineUsers.length}</Role>
            {offlineUsers.map((user) => (
                <UserRow
                    user={user}
                    isBot={false}/>
            ))}
        </Container>
    );
};

export default UserList;
