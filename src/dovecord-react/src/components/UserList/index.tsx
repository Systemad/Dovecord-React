import React from "react";

import { Container, Role, User, Avatar } from "./styles";

interface UserProps {
    nickname: string;
    isBot?: boolean;
}

const UserRow: React.FC<UserProps> = ({ nickname, isBot }) => {
    return (
        <User>
            <Avatar className={isBot ? "bot" : ""} />

            <strong>{nickname}</strong>

            {isBot && <span>Bot</span>}
        </User>
    );
};

const UserList: React.FC = () => {
    return (
        <Container>
            <Role>Disponível - 1</Role>

            <UserRow nickname="Gabriel Shelby" />

            <Role>Offline - 18</Role>
            <UserRow nickname="Matheus Shelby" isBot />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
            <UserRow nickname="Fulano Shelby" />
        </Container>
    );
};

export default UserList;
