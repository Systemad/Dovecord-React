import React, {FormEvent, useEffect} from "react";
import UserInfo from "./UserInfo";
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {getCurrentServer} from "../../redux/features/servers/serverSlice";
import {useServerGetUsersOfServerQuery, useUserGetMeQuery} from "../../redux/webApi";
import {skipToken} from "@reduxjs/toolkit/query";
import UserList from "./UserList";
import {useNavigate} from "react-router-dom";

export const UserComponent = () => {

    const currentServer = useAppSelector(getCurrentServer);
    const serverId = currentServer?.id;
    const {data: users} = useServerGetUsersOfServerQuery(serverId ? {serverId: serverId} : skipToken);
    const onlineUsers = users?.filter(user => user.isOnline === true);
    const offlineUsers = users?.filter(user => user.isOnline === false);
    const {data: user} = useUserGetMeQuery();
    const navigate = useNavigate();
    const NavigateSettings = () => {
        navigate("/settings");
    }

    const NavigateToSettings = (e: FormEvent<HTMLInputElement>): void => {
        e.preventDefault();
        navigate("/settings");
    }

    //const usersStatus = useAppSelector(selectUsersStatus)

    //const users = UserDto;
    //         <UserList/>

    /*
                {currentServer?.channels.map((channel) => (
                <ChannelButton
                    key={channel.channel.id}
                    click={() => setChannel(channel)}
                    channel={channel.channel} />
            ))}
     */
    return (
        <>
            <UserInfo
                user={user}
                onClick={() => NavigateSettings()}
            />
            <UserList
                onlineUsers={onlineUsers}
                offlineUsers={offlineUsers}
            />
        </>
    );
};

export default UserComponent;