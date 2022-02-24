import {ChannelDto, ChannelMessageDto} from "../../../services/types";
import {createAsyncThunk, createSlice, PayloadAction} from "@reduxjs/toolkit";
import {RootState} from "../../store";
import {getChannels} from "../../../services/services";


type AddMessage = {
    channelId: string
    message: ChannelMessageDto[]
}

type State = {
    channels: ChannelState[],
}

type ChannelState = {
    channel: ChannelDto
    messages: ChannelMessageDto[]
}

const initialState: State = {
    channels: []
}

export const getChannelsAsync = createAsyncThunk(
    'channels/getChannels',
    async () => {
        const resp = await getChannels();
        const fetchedChannels = resp.data
        return { fetchedChannels };
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
            //data[channel] = {...state.channels[channel], messages: [...message, ...state.channels[channel!].messages]}
            data[channel] = {...state.channels[channel], messages: [message, ...state.channels[channel!].messages]}
            return {
                ...state,
                channels: data
            }
        }
    }
})

export const { setChannels, addMessageToChannel } = channelSlice.actions;

export const selectChannels = (state: RootState) => state.chatchannels.channels;

export default channelSlice.reducer