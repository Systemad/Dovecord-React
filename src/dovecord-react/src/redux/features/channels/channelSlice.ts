import {ChannelDto, ChannelMessageDto} from "../../../services/types";
import {createAsyncThunk, createSlice, PayloadAction} from "@reduxjs/toolkit";
import {RootState} from "../../store";
import {getChannels, getChannelsId, getMessagesChannelId} from "../../../services/services";


type DeleteMessage = {
    channelId: string
    messageId: string
}

type State = {
    channels: ChannelState[],
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

type ChannelState = {
    channel: ChannelDto
    messages: ChannelMessageDto[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

const initialState: State = {
    channels: [],
    loading: 'idle'
}

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
            const channelDataFetch = await getChannelsId(data[i].id!);
            const fetchChannelMessages = await getMessagesChannelId(data[i].id!);

            const newChannel: ChannelState = {
                channel: channelDataFetch.data,
                messages: fetchChannelMessages.data,
                loading: 'succeeded'
            }
            newChannelData.channels.push(newChannel);

        }
        return newChannelData;
        //dispatch(setChannels(newChannelData));
    }
)

export const channelSlice = createSlice({
    name: 'channels',
    initialState,
    reducers: {
        setChannels: (state, action: PayloadAction<State>) => {
            return {...state, ...action.payload}
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

            //const channelId = action.payload.channelId;
            const data = [...state.channels];
            //const message = action.payload;
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
    }
})

export const { setChannels, addMessageToChannel, deleteMessageFromChannel } = channelSlice.actions;

export const selectChannels = (state: RootState) => state.chatchannels.channels;
export const selectStatus = (state: RootState) => state.chatchannels.loading;

export default channelSlice.reducer