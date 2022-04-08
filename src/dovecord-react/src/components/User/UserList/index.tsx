import React from "react";

import { Container, Role, User, Avatar } from "./styles";

import {useAppDispatch} from "../../../redux/hooks";
import {UserDto} from "../../../redux/webApi";

interface UserProps {
    user: UserDto;
    //click(): void;
}


// Set avatar badge like avatar but for online status
const UserRow: React.FC<UserProps> = ({ user }) => {
    return (
        <User>
            <Avatar className={user.bot ? "bot" : ""} />

            <strong>{user.username}</strong>

            {user.bot && <span>Bot</span>}
        </User>
    );
};

interface UserListProps {
    onlineUsers?: UserDto[];
    offlineUsers?: UserDto[];
}

const UserList: React.FC<UserListProps> = ({onlineUsers, offlineUsers}) => {

    /*
    const setUser = async (user: UserState) => {
        dispatch(setCurrentUser(user.user));
        if(user.loading === 'idle'){
            dispatch(fetchUserMessagesAsync(user.user.id!));
        }
    }
    click={() => setUser(user)}
    */

    return (
        <Container>
            <Role>Online - {onlineUsers?.length}</Role>

            {onlineUsers?.map((user) => (
                <UserRow
                    key={user.id}
                    user={user}/>
                ))}

            <Role>Offline - {offlineUsers?.length}</Role>
                {offlineUsers?.map((user) => (
                    <UserRow
                        key={user.id}
                        user={user}/>
                ))}
        </Container>
    );
};

export default UserList;