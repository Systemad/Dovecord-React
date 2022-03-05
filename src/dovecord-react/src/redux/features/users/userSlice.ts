import {createAsyncThunk, createSlice, PayloadAction} from "@reduxjs/toolkit";
import {RootState} from "../../store";
import {PrivateMessageDto, UserDto} from "../../../services/types";
import {getPmessagesUserId, getUsers} from "../../../services/services";

type DeleteMessage = {
    receiverId: string
    messageId: string
}

type State = {
    users: UserState[],
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
    currentUser?: UserDto // TODO: Find way to set UserDto or ChannelDto in uiSlice, or dirty and dispatch here and to uiSlice
}

type UserState = {
    user: UserDto
    messages: PrivateMessageDto[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

const initialState: State = {
    users: [],
    loading: 'idle',
    currentUser: {}
}

/*
type State = {
    channels: ChannelState[],
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
    currentChannel?: ChannelDto
}

type ChannelState = {
    channel: ChannelDto
    messages: ChannelMessageDto[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

const initialState: State = {
    channels: [],
    loading: 'idle',
    currentChannel: {}
}

 */

export const fetchUserMessagesAsync = createAsyncThunk(
    'users/id',
    async (userId: string) => {
        const fetchMessages = await getPmessagesUserId(userId);
        return fetchMessages.data;
    }
)

export const fetchUsersAsync = createAsyncThunk(
    'users/fetchUsers',
    async () => {
        const users = await getUsers();
        const data = users.data;
        let newUserData: State = {
            users: [],
            loading: 'pending'
        }

        // TODO: Dont create initially? Create on click
        for (let i = 0; i < data.length; i++){
            const newChannel: UserState = {
                user: data[i],
                messages: [],
                loading: 'idle'
            }
            newUserData.users.push(newChannel);
        }
        return newUserData;
    }
)

export const userSlice = createSlice({
    name: 'users',
    initialState,
    reducers: {
        setUsers: (state, action: PayloadAction<State>) => {
            return {...state, ...action.payload}
        },
        setCurrentUser: (state, action: PayloadAction<UserDto>) => {
            state.currentUser = action.payload;
        },
        addMessageToUser: (state, action: PayloadAction<PrivateMessageDto>) => {
            //const { channelId, message } = action.payload;
            const receiverUserId = action.payload.receiverUserId;
            const data = [...state.users];
            const message = action.payload;
            const user = data.findIndex((user) => user.user.id === receiverUserId);
            data[user] = {...state.users[user], messages: [...state.users[user!].messages, message]}
            /*
            return {
                ...state,
                users: data,
            }
             */
        },
        deleteMessageFromUser: (state, action: PayloadAction<DeleteMessage>) => {
            const { receiverId, messageId } = action.payload;
            const data = [...state.users];
            const user = data.findIndex((user) => user.user.id === receiverId);
            const newMessages = data[user].messages.filter((msg) => msg.id !== messageId);
            data[user] = {...state.users[user], messages: newMessages}
            /*
            return {
                ...state,
                users: data
            }
             */
        }
    },
    extraReducers: (builder) => {
        builder.addCase(fetchUsersAsync.pending, (state, action) => {
            state.loading = 'pending'
        })
        builder.addCase(fetchUsersAsync.fulfilled, (state, action) => {
            state.users = state.users.concat(action.payload.users)  //{...action.payload}
            state.loading = 'succeeded'
        })
        builder.addCase(fetchUserMessagesAsync.pending, (state, action) => {
            const data = [...state.users];
            const user = data.findIndex((channel) => channel.user.id === state.currentUser!.id);
            data[user].loading = 'pending'
        })
        builder.addCase(fetchUserMessagesAsync.fulfilled, (state, action) => {
            const data = [...state.users];
            const user = data.findIndex((channel) => channel.user.id === state.currentUser!.id);
            state.users[user].messages = action.payload;
            data[user].loading = 'succeeded'
        })
    }
})

export const { setUsers, addMessageToUser, deleteMessageFromUser, setCurrentUser } = userSlice.actions;
export const selectUsers = (state: RootState) => state.users.users;

export const selectUsersStatus = (state: RootState) => state.users.loading;
export const selectOnlineUsers = (state: RootState) => state.users.users.filter((user) => user.user.isOnline === true);
export const selectOfflineUsers = (state: RootState) => state.users.users.filter((user) => user.user.isOnline === true);

export const getCurrentUser = (state: RootState) => state.users.currentUser;
export default userSlice.reducer