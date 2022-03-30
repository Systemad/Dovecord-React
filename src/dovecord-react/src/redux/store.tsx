import {configureStore, combineReducers, ConfigureStoreOptions} from '@reduxjs/toolkit'
import {emptySplitApi} from "./emptyApi";
import {webApi} from "./webApi";

export const createStore = (options?: ConfigureStoreOptions['preloadedState'] | undefined) =>
    configureStore({
        reducer: {
            [emptySplitApi.reducerPath]: emptySplitApi.reducer,
        },
        middleware: (getDefaultMiddleware) =>
            getDefaultMiddleware().concat(emptySplitApi.middleware),
        ...options,
    });

export const store = createStore();

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;