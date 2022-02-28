import React from "react";
import UserInfo from "./UserInfo";
import UserList from "./UserList";
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {selectUsers} from "../../redux/features/users/userSlice";
import {UserData} from "./UserInfo/styles";

const UserComponent: React.FC = () => {
    const dispatch = useAppDispatch();
    const onlineUsers = useAppSelector(selectUsers)
    const offlineUsers = useAppSelector(selectUsers)

    //const users = UserDto;
    //         <UserList/>
    return (
        <>
        <UserInfo/>
        </>
    );
};

export default UserComponent;