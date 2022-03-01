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

    }
})

export const getCurrentChannel = (state: RootState) => state.ui.currentChannel;
export default uiSlice.reducer;