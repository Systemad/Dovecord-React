import { configureStore, combineReducers  } from '@reduxjs/toolkit'
import ServerSlice from "./features/servers/serverSlice"

export const store = configureStore({
    reducer: {
        servers: ServerSlice
    }
})


// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch