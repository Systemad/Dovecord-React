import { configureStore } from '@reduxjs/toolkit'
import ChannelSlice from "./features/channels/channelSlice";
import UiSlice from "./uiSlice";
import UserSlice from "./features/users/userSlice";

export const store = configureStore({
    reducer: {
        chatchannels: ChannelSlice,
        users: UserSlice,
        ui: UiSlice
    }
})

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch