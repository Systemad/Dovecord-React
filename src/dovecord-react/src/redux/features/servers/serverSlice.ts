import {ChannelDto, ChannelMessageDto, ServerDto, UserDto} from "../../../services/types";
import {createAsyncThunk, createSelector, createSlice, PayloadAction} from "@reduxjs/toolkit";
import {RootState} from "../../store";
import {
    getV1MessagesChannelId, getV1ServersMeServers,
    getV1ServersServerId,
    getV1ServersServerIdChannels
} from "../../../services/services";

type DeleteMessage = {
    channelId: string
    serverId?: string
    messageId: string
}

type CurrentState = {
    currentServer?: ServerDto,
    currentChannel?: ChannelDto
}

type State = {
    servers: ServerState[]
    directMessages: ChannelState[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
    currentState: CurrentState
}

export type ServerState = {
    server: ServerDto;
    channels: ChannelState[]
    users: UserState[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

export type ChannelState = {
    channel: ChannelDto
    messages: ChannelMessageDto[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

export type UserState = {
    user: UserDto
    //messages?: ChannelMessageDto[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

const initialState: State = {
    servers: [],
    directMessages: [],
    loading: 'idle',
    currentState: {}
}

export const fetchServersAsync = createAsyncThunk(
    'servers/me',
    async () => {
        const servers = await getV1ServersMeServers();
        const serverData = servers.data;

        const newState: State = {
            currentState: {},
            directMessages: [],
            loading: 'idle',
            servers: []
        }

        for(let i = 0; i < serverData.length; i++){
            const serverFetch = await getV1ServersServerId(serverData[i].id!);

            const newServerState: ServerState =  {
                channels: [],
                loading: 'idle',
                server: serverFetch.data,
                users: []
            }
            newState.servers.push(newServerState);
        }
        return newState;
    }
)

export const fetchChannelMessagesAsync = createAsyncThunk(
    'servers/id',
    async (channelId: string) => {
        const fetchMessages = await getV1MessagesChannelId(channelId);
        return fetchMessages.data;
    }
)

export const fetchChannelsAsync = createAsyncThunk(
    'servers/fetchChannels',
    async (serverId: string) => {
        const serverInfo = await getV1ServersServerId(serverId);
        const serverInfoData = serverInfo.data;

        const channels = await getV1ServersServerIdChannels(serverId);
        const channelData = channels.data;

        const newServerState: ServerState =  {
            channels: [],
            loading: 'succeeded',
            server: serverInfoData,
            users: []
        }

        for(let i = 0; i < channelData.length; i++){
            const channelState: ChannelState = {
                channel: channelData[i],
                loading: 'succeeded',
                messages: []
            }
            newServerState.channels.push(channelState);
        }
        return newServerState;
    }
)



export const serverSlice = createSlice({
    name: 'servers',
    initialState,
    reducers: {
        setCurrentServer: (state, action: PayloadAction<ServerDto>) => {
            state.currentState.currentServer = action.payload;
        },
        setCurrentChannel: (state, action: PayloadAction<ChannelDto>) => {
            state.currentState.currentChannel = action.payload;
        },
        addServer: (state, action: PayloadAction<ServerState>) => {
          state.servers.push(action.payload)
        },
        addChannel: (state, action: PayloadAction<ChannelState>) => {
            const findServer = state.servers.findIndex((server) => server.server.id == action.payload.channel.serverId);
            state.servers[findServer].channels.push(action.payload);
        },
        addMessageToChannel: (state, action: PayloadAction<ChannelMessageDto>) => {
            const {serverId, channelId} = action.payload;
            const serverData = [...state.servers];
            const findServer = serverData.findIndex((server) => server.server.id === serverId);
            const channelToAdd = state.servers[findServer].channels.findIndex((channel) => channel.channel.id === channelId)
            state.servers[findServer].channels[channelToAdd].messages.push(action.payload);
        },
        addMessageToUserChannel: (state, action: PayloadAction<ChannelMessageDto>) => {
            const {channelId} = action.payload;
            const serverData = [...state.directMessages];
            const channelToAdd = state.directMessages.findIndex((channel) => channel.channel.id === channelId)
            state.directMessages[channelToAdd].messages.push(action.payload);
        },
        deleteMessageFromChannel: (state, action: PayloadAction<DeleteMessage>) => {
            const {serverId, channelId, messageId} = action.payload;
            //const serverData = [...state.servers];
            const findServer = state.servers.findIndex((server) => server.server.id === serverId);
            const channelToRemove = state.servers[findServer].channels.findIndex((channel) => channel.channel.id === channelId)
            const findMessage = state.servers[findServer].channels[channelToRemove].messages.find((msg) => msg.id === messageId);
            state.servers[findServer].channels[channelToRemove].messages =
                state.servers[findServer].channels[channelToRemove].messages.filter((msg) => msg.id !== findMessage);
            //const newMessages = data[channel].messages.filter((msg) => msg.id !== messageId);
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchServersAsync.pending, (state, action) => {
            state.loading = 'pending'
        })
        builder.addCase(fetchServersAsync.fulfilled, (state, action) => {
            state.servers = action.payload.servers;
        })
        builder.addCase(fetchChannelsAsync.pending, (state, action) => {
            state.loading = 'pending'
        })
        builder.addCase(fetchChannelsAsync.fulfilled, (state, action) => {
            const serverId = state.currentState.currentServer!.id;
            const serverData = [...state.servers];
            const findServer = serverData.findIndex((server) => server.server.id === serverId);
            const {channels, server} = action.payload;
            if(!state.servers[findServer]){ // Ensure nested array exists
                state.servers[findServer].channels = [];
            }
            state.servers[findServer].server = server;
            state.servers[findServer].channels = channels;
            state.servers[findServer].loading = "succeeded";
        })
        builder.addCase(fetchChannelMessagesAsync.pending, (state, action) => {
            //const hey = store.getState().ui.currentChannel
            //const data = [...state.servers];
            //const channel = data.findIndex((channel) => channel.channel.id === action.payload.);
            //data[channel].loading = 'pending'
        })
        builder.addCase(fetchChannelMessagesAsync.fulfilled, (state, action) => {
            const serverId = state.currentState.currentServer!.id;
            const channelId = state.currentState.currentChannel!.id;

            const serverData = [...state.servers];
            const findServer = serverData.findIndex((server) => server.server.id === serverId);
            const channelToAdd = state.servers[findServer].channels.findIndex((channel) => channel.channel.id === channelId)

            state.servers[findServer].channels[channelToAdd].messages = action.payload;
            state.servers[findServer].channels[channelToAdd].loading = 'succeeded'
        })
    }
})

export const {addMessageToChannel, addServer, deleteMessageFromChannel, setCurrentChannel, setCurrentServer, addChannel} = serverSlice.actions;

export const selectMainState = (state: RootState) => state.servers;
export const selectCurrentState = (state: RootState) => state.servers.currentState;
export const selectServers = (state: RootState) => state.servers.servers;
export const selectServersStatus = (state: RootState) => state.servers.loading;

//export const selectChannels = (state: RootState) => state.servers.;
//export const getCurrentChannel = (state: RootState) => state.servers.currentChannel;

export default serverSlice.reducer