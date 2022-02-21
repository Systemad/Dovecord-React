import {ChannelDto} from "../services/types";
import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {RootState} from "./store";

type State = {
    currentChannel?: ChannelDto
}
const initialState: State = {
    currentChannel: {}
}

export const uiSlice = createSlice({
    name: 'ui',
    initialState,
    reducers: {
        setCurrentChannel: (state, action: PayloadAction<ChannelDto>) => {
            state.currentChannel = action.payload;
        }
    }
})

export const { setCurrentChannel } = uiSlice.actions
export const getCurrentChannel = (state: RootState) => state.ui.currentChannel;
export default uiSlice.reducer;