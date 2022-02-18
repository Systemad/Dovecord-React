import {configureStore, createSlice, PayloadAction} from '@reduxjs/toolkit'
import { ChannelDto } from "../../services/types";
import {channelSlice} from "./channels/channelSlice";


const store = configureStore({
    reducer: {
        currentChannel: channelSlice.reducer
    }
})

export type RootState = ReturnType<typeof store.getState>;
export default store;