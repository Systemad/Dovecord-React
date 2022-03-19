import { configureStore, combineReducers  } from '@reduxjs/toolkit'
import ServerSlice from "./features/servers/serverSlice"
import {emptySplitApi} from "./emptyApi";
import {webApi} from "./webApi";

export const store = configureStore({
    reducer: {
        servers: ServerSlice,
        //[emptySplitApi.reducerPath]: emptySplitApi.reducer
    },
    //middleware: (getDefaultMiddleware) =>
    //    getDefaultMiddleware().concat(emptySplitApi.middleware)
})

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch