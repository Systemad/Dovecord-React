import {ChannelDto, ChannelMessageDto, PrivateMessageDto, UserDto} from "../../../services/types";
import {$CombinedState, createAsyncThunk, createSlice, PayloadAction} from "@reduxjs/toolkit";
import {RootState} from "../../store";
import {getChannels, getMessagesChannelId, getPmessagesUserId, getUsers} from "../../../services/services";

type DeleteMessage = {
    channelId: string
    messageId: string
}

type State = {
    channels: ChannelState[]
    users: UserState[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
    currentChannel?: ChannelDto | UserDto
}

type UserState = {
    user: UserDto
    messages: PrivateMessageDto[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

type ChannelState = {
    channel: ChannelDto
    messages: ChannelMessageDto[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

const initialState: State = {
    channels: [],
    users: [],
    loading: 'idle',
    currentChannel: {}
}

export const fetchChannelMessagesAsync = createAsyncThunk(
    'channels/id',
    async (channelId: string) => {
        const fetchMessages = await getMessagesChannelId(channelId);
        return fetchMessages.data;
    }
)

export const fetchUserMessagesAsync = createAsyncThunk(
    'users/id',
    async (userId: string) => {
        const fetchMessages = await getPmessagesUserId(userId);
        return fetchMessages.data;
    }
)

export const fetchChannelsAsync = createAsyncThunk(
    'channels/fetchChannels',
    async () => {
        const channels = await getChannels();
        const users = await getUsers();

        const userData = users.data;
        const data = channels.data;

        let newChannelData: State = {
            channels: [],
            users: [],
            loading: 'pending'
        }

        // TODO: Dont create initially? Create on click
        for (let i = 0; i < data.length; i++){
            const newChannel: ChannelState = {
                channel: data[i],
                messages: [],
                loading: 'idle'
            }
            newChannelData.channels.push(newChannel);
        }

        for (let i = 0; i < userData.length; i++){
            const newUserState: UserState = {
                user: userData[i],
                messages: [],
                loading: 'idle'
            }
            newChannelData.users.push(newUserState);
        }
        return newChannelData;
    }
)



export const channelSlice = createSlice({
    name: 'channels',
    initialState,
    reducers: {
        setChannels: (state, action: PayloadAction<State>) => {
            return {...state, ...action.payload}
        },
        setCurrentChannel: (state, action: PayloadAction<ChannelDto>) => {
            state.currentChannel = action.payload;
        },
        addMessageToChannel: (state, action: PayloadAction<ChannelMessageDto>) => {
            //const { channelId, message } = action.payload;
            const channelId = action.payload.channelId;
            const data = [...state.channels];
            const message = action.payload;
            const channel = data.findIndex((channel) => channel.channel.id === channelId);
            data[channel] = {...state.channels[channel], messages: [...state.channels[channel!].messages, message]}
            return {
                ...state,
                channels: data,
            }
        },
        deleteMessageFromChannel: (state, action: PayloadAction<DeleteMessage>) => {
            const { channelId, messageId } = action.payload;
            const data = [...state.channels];
            const channel = data.findIndex((channel) => channel.channel.id === channelId);
            const newMessages = data[channel].messages.filter((msg) => msg.id !== messageId);
            data[channel] = {...state.channels[channel], messages: newMessages}
            return {
                ...state,
                channels: data
            }
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchChannelsAsync.pending, (state, action) => {
            state.loading = 'pending'
        })
        builder.addCase(fetchChannelsAsync.fulfilled, (state, action) => {
            state.channels = state.channels.concat(action.payload.channels)  //{...action.payload}
            state.loading = 'succeeded'
        })
        builder.addCase(fetchChannelMessagesAsync.pending, (state, action) => {
            //const hey = store.getState().ui.currentChannel
            //const data = [...state.channels];
            //const channel = data.findIndex((channel) => channel.channel.id === action.payload.);
            //data[channel].loading = 'pending'
        })
        builder.addCase(fetchChannelMessagesAsync.fulfilled, (state, action) => {
            //const currentChannel = st
            const data = [...state.channels];
            const channel = data.findIndex((channel) => channel.channel.id === state.currentChannel?.id);
            state.channels[channel].messages = action.payload;
            data[channel].loading = 'succeeded'
        })
    }
})

export const { setChannels, addMessageToChannel, deleteMessageFromChannel, setCurrentChannel } = channelSlice.actions;

export const selectChannels = (state: RootState) => state.channels.channels;
export const selectStatus = (state: RootState) => state.channels.loading;
//export const getCurrentChannel = (state: RootState) => state.channels.currentChannel;

export default channelSlice.reducer