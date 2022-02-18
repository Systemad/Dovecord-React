import {ChannelDto} from "../../../services/types";
import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {RootState} from "../store";

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
export const selectChannel = (state: RootState) => state.currentChannel;
export default channelSlice