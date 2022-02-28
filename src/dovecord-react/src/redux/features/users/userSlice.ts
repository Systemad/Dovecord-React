import {ChannelDto, ChannelMessageDto, PrivateMessageDto, UserDto} from "../../../services/types";
import {createAsyncThunk, createSlice, PayloadAction} from "@reduxjs/toolkit";
import {RootState} from "../../store";
import {getChannels} from "../../../services/services";


type DeleteMessage = {
    receiverId: string
    messageId: string
}

type State = {
    users: UserState[],
}

type UserState = {
    users: UserDto
    messages: ChannelMessageDto[]
}

const initialState: State = {
    users: []
}

export const userSlice = createSlice({
    name: 'users',
    initialState,
    reducers: {
        setUsers: (state, action: PayloadAction<State>) => {
            return {...state, ...action.payload}
        },
        addMessageToPM: (state, action: PayloadAction<PrivateMessageDto>) => {
            //const { channelId, message } = action.payload;

            const receiverUserId = action.payload.receiverUserId;
            const data = [...state.users];
            const message = action.payload;
            const user = data.findIndex((user) => user.users.id === receiverUserId);
            data[user] = {...state.users[user], messages: [...state.users[user!].messages, message]}
            return {
                ...state,
                users: data
            }
        },
        deleteMessageFromPM: (state, action: PayloadAction<DeleteMessage>) => {
            const { receiverId, messageId } = action.payload;

            //const channelId = action.payload.channelId;
            const data = [...state.users];
            //const message = action.payload;
            const user = data.findIndex((user) => user.users.id === receiverId);
            const newMessages = data[user].messages.filter((msg) => msg.id !== messageId);
            data[user] = {...state.users[user], messages: newMessages}
            return {
                ...state,
                users: data
            }
        }
    }
})

export const { setUsers, addMessageToPM, deleteMessageFromPM } = userSlice.actions;

export const selectUsers = (state: RootState) => state.users.users;

export default userSlice.reducer