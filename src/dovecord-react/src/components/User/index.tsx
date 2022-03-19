import React, {useEffect} from "react";
import UserInfo from "./UserInfo";
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {UserData} from "./UserInfo/styles";
import {fetchChannelsAsync} from "../../redux/features/servers/serverSlice";
/*
export const UserComponent: React.FC = () => {
    const dispatch = useAppDispatch();
    const onlineUsers = useAppSelector(selectUsers)
    const offlineUsers = useAppSelector(selectUsers)
    const usersStatus = useAppSelector(selectUsersStatus)
    useEffect(() => {
        if(usersStatus === 'idle'){
            dispatch(fetchUsersAsync());
        }
    }, []);
    //const users = UserDto;
    //         <UserList/>
    return (
        <>
            <UserList onlineUsers={onlineUsers} />
            <UserInfo/>
        </>
    );
};

export default UserComponent;
 */