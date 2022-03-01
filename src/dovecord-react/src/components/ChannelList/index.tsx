import ChannelButton from "../ChannelButton";
import { Container, Category, AddCategoryIcon } from "./styles";
import {ChannelDto, ChannelMessageDto} from "../../services/types";
import {
    fetchChannelMessagesAsync,
    fetchChannelsAsync,
    selectChannels,
    selectStatus
} from "../../redux/features/channels/channelSlice"
import {useAppDispatch, useAppSelector} from "../../redux/hooks";
import { setCurrentChannel } from "../../redux/features/channels/channelSlice";
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
        dispatch(setCurrentChannel(channel.channel));
        if(channel.loading === 'idle'){
            dispatch(fetchChannelMessagesAsync(channel.channel.id!));
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