import React, {useEffect} from "react";
import ChannelData from "./ChannelData";
import ChannelInfo from "./ChannelInfo";
import ChannelList from "./ChannelList";


export const UserComponent = () => {
    /*
    useEffect(() => {

    }, []);
    */
    return (
        <>
            <ChannelData/>
            <ChannelInfo/>
            <ChannelList/>
        </>
    );
};

export default UserComponent;