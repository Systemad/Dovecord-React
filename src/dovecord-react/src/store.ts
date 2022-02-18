import {configureStore, createSlice, PayloadAction} from '@reduxjs/toolkit'
import { ChannelDto } from "./services/types";

interface ChannelSliceState {
    currentChannel: ChannelDto;
}

const initialState: ChannelSliceState = {
    currentChannel: {},
}

export const channelSlice = createSlice({
    name: 'counter',
    initialState,
    reducers: {
        setChannel: (state, action: PayloadAction<ChannelDto>) => {
            state.currentChannel = action.payload;
        }
    }
})

export const { setChannel } = channelSlice.actions;

const store = configureStore({
    reducer: {
        currentChannel: channelSlice.reducer
    }
})

type RootState = ReturnType<typeof store.getState>;

export const selectChannel = (state: RootState) => state.currentChannel;

export default store;