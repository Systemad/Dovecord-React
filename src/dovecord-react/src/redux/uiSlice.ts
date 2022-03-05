import {ChannelDto, UserDto} from "../services/types";
import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {RootState} from "./store";

type State = {
    currentChannel?: ChannelDto | UserDto
}
const initialState: State = {
    currentChannel: {}
}

export const uiSlice = createSlice({
    name: 'ui',
    initialState,
    reducers: {
        setCurrentChannel: (state, action: PayloadAction<ChannelDto | UserDto>) => {
            state.currentChannel = action.payload;
        }
    }
})

export const getCurrentChannel = (state: RootState) => state.ui.currentChannel;
export default uiSlice.reducer;