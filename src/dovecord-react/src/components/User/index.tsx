import React, {useEffect} from "react";
import UserInfo from "./UserInfo";
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import {UserData} from "./UserInfo/styles";
//import {fetchChannelsAsync, selectCurrentState, selectServers} from "../../redux/features/servers/serverSlice";
import UserList from "./UserList";
import {useAccount, useMsal} from "@azure/msal-react";

export const UserComponent: React.FC = () => {

    const dispatch = useAppDispatch();



    //const currentState = useAppSelector(selectCurrentState);
    //const currentServer = useAppSelector(selectServers).find((server) => server.server.id === currentState.currentServer?.id);

    //const onlineUsers = currentServer?.users.filter(user => user.isOnline == true);
    //const offlineUsers = currentServer?.users.filter(user => user.isOnline == false);
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

            <UserInfo/>
        </>
    );
};

export default UserComponent;


/*
            <UserList
                onlineUsers={onlineUsers}
                offlineUsers={offlineUsers}
            />
 */