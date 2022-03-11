import React from "react";
/*
import { Container, Role, User, Avatar } from "./styles";

import {useAppDispatch} from "../../../redux/hooks";

interface UserProps {
    user: UserDto;
    isBot?: boolean;
    click(): void;
}


// Set avatar badge like avatar but for online status
const UserRow: React.FC<UserProps> = ({ user, isBot, click }) => {
    return (
        <User onClick={click}>
            <Avatar className={isBot ? "bot" : ""} />

            <strong>{user.name}</strong>

            {isBot && <span>Bot</span>}
        </User>
    );
};

type UserState = {
    user: UserDto
    messages: PrivateMessageDto[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

interface UserListProps {
    onlineUsers: UserState[];
    //offlineUsers: UserDto[];
}

const UserList: React.FC<UserListProps> = ({onlineUsers}) => {
    const dispatch = useAppDispatch();

    const setUser = async (user: UserState) => {
        dispatch(setCurrentUser(user.user));
        if(user.loading === 'idle'){
            dispatch(fetchUserMessagesAsync(user.user.id!));
        }
    }

    return (
        <Container>
            <Role>Online - {onlineUsers.length}</Role>

            {onlineUsers.map((user) => (
                <UserRow
                    user={user.user}
                    isBot={false}
                    click={() => setUser(user)}/>
                ))}
        </Container>
    );
};

export default UserList;

 */
