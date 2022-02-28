import ChannelButton from "../ChannelButton";
import { Container, Category, AddCategoryIcon } from "./styles";
import {ChannelDto, ChannelMessageDto} from "../../services/types";
import {fetchChannelsAsync, selectChannels, selectStatus} from "../../redux/features/channels/channelSlice"
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import { setCurrentChannel } from "../../redux/uiSlice";
import {useEffect} from "react";

type ChannelState = {
    channel: ChannelDto
    messages: ChannelMessageDto[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

const ChannelList = () => {
    const dispatch = useAppDispatch();
    const channels = useAppSelector(selectChannels);
    const channelStatus = useAppSelector(selectStatus)

    const setChannel = async (channel: ChannelState) => {
        if(channel.loading === 'succeeded'){
            dispatch(setCurrentChannel(channel.channel));
        }
        if(channel.loading === 'idle'){

            console.log("Fetch channel and state it")
            //dispatch(setCurrentChannel(channel.channel));
        }
    }
    useEffect(() => {
        if(channelStatus === 'idle'){
            dispatch(fetchChannelsAsync());
        }
    }, []);

    return (
        <Container>
            <Category>
                <span>Text Channels</span>
                <AddCategoryIcon />
            </Category>

            {channels.map((channel) => (
                <ChannelButton
                    click={() => setChannel(channel)}
                    channel={channel.channel} />
            ))}
        </Container>
    );
};

export default ChannelList;