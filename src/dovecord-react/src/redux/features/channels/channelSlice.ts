import {ChannelDto, ChannelMessageDto} from "../../../services/types";
import {createAsyncThunk, createSlice, PayloadAction} from "@reduxjs/toolkit";
import {RootState} from "../../store";
import {getChannels, getMessagesChannelId} from "../../../services/services";

type DeleteMessage = {
    channelId: string
    messageId: string
}

type State = {
    channels: ChannelState[],
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
    currentChannel?: ChannelDto
}

type ChannelState = {
    channel: ChannelDto
    messages: ChannelMessageDto[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

const initialState: State = {
    channels: [],
    loading: 'idle',
    currentChannel: {}
}


export const fetchChannelMessagesAsync = createAsyncThunk(
    'channels/id',
    async (channelId: string) => {
        const fetchMessages = await getMessagesChannelId(channelId);
        return fetchMessages.data;
    }
)

export const fetchChannelsAsync = createAsyncThunk(
    'channels/fetchChannels',
    async () => {
        const channels = await getChannels();
        const data = channels.data;
        let newChannelData: State = {
            channels: [],
            loading: 'pending'
        }

        for (let i = 0; i < data.length; i++){ // TODO: set max 5
            const newChannel: ChannelState = {
                channel: data[i],
                messages: [],
                loading: 'idle'
            }
            newChannelData.channels.push(newChannel);
        }
        return newChannelData;
    }
)

export const channelSlice = createSlice({
    name: 'channels',
    initialState,
    reducers: {
        setChannels: (state, action: PayloadAction<State>) => {
            return {...state, ...action.payload}
        },
        setCurrentChannel: (state, action: PayloadAction<ChannelDto>) => {
            state.currentChannel = action.payload;
        },
        addMessageToChannel: (state, action: PayloadAction<ChannelMessageDto>) => {
            //const { channelId, message } = action.payload;
            const channelId = action.payload.channelId;
            const data = [...state.channels];
            const message = action.payload;
            const channel = data.findIndex((channel) => channel.channel.id === channelId);
            data[channel] = {...state.channels[channel], messages: [...state.channels[channel!].messages, message]}
            return {
                ...state,
                channels: data,
            }
        },
        deleteMessageFromChannel: (state, action: PayloadAction<DeleteMessage>) => {
            const { channelId, messageId } = action.payload;
            const data = [...state.channels];
            const channel = data.findIndex((channel) => channel.channel.id === channelId);
            const newMessages = data[channel].messages.filter((msg) => msg.id !== messageId);
            data[channel] = {...state.channels[channel], messages: newMessages}
            return {
                ...state,
                channels: data
            }
        }
    },
    extraReducers: (builder) => {
        builder.addCase(fetchChannelsAsync.pending, (state, action) => {
            state.loading = 'pending'
        })
        builder.addCase(fetchChannelsAsync.fulfilled, (state, action) => {
            state.channels = state.channels.concat(action.payload.channels)  //{...action.payload}
            state.loading = 'succeeded'
        })
        builder.addCase(fetchChannelMessagesAsync.pending, (state, action) => {
            const data = [...state.channels];
            const channel = data.findIndex((channel) => channel.channel.id === state.currentChannel!.id);
            data[channel].loading = 'pending'
        })
        builder.addCase(fetchChannelMessagesAsync.fulfilled, (state, action) => {
            const data = [...state.channels];
            const channel = data.findIndex((channel) => channel.channel.id === state.currentChannel!.id);
            state.channels[channel].messages = action.payload;
            data[channel].loading = 'succeeded'
        })
    }
})

export const { setChannels, addMessageToChannel, deleteMessageFromChannel, setCurrentChannel } = channelSlice.actions;

export const selectChannels = (state: RootState) => state.chatchannels.channels;
export const selectStatus = (state: RootState) => state.chatchannels.loading;
export const getCurrentChannel = (state: RootState) => state.chatchannels.currentChannel;

export default channelSlice.reducer