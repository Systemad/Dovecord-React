import {ChannelDto, ChannelMessageDto} from "../../../services/types";
import {createAsyncThunk, createSlice, PayloadAction} from "@reduxjs/toolkit";
import {RootState} from "../../store";
import {getChannels} from "../../../services/services";


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
        }
    }
})

export const { setChannels } = channelSlice.actions;

export const selectChannels = (state: RootState) => state.chatchannels.channels;

export default channelSlice.reducer