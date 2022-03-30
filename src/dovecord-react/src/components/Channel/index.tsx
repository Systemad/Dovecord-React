import React, {useEffect} from "react";
import ChannelData from "./ChannelData";
import ChannelInfo from "./ChannelInfo";
import ChannelList from "./ChannelList";


export const ChannelComponent = () => {
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

export default ChannelComponent;