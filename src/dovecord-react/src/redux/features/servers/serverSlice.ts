import {ChannelDto, ChannelMessageDto, ServerDto, UserDto} from "../../../services/types";
import {createAsyncThunk, createEntityAdapter, createSelector, createSlice, PayloadAction} from "@reduxjs/toolkit";
import {RootState} from "../../store";
import {
    getV1MessagesChannelId, getV1ServersMeServers,
    getV1ServersServerId,
    getV1ServersServerIdChannels, getV1ServersServerIdUsers, getV1Users
} from "../../../services/services";


type CurrentState = {
    currentServer?: ServerDto,
    currentChannel?: ChannelDto
}

type State = {
    currentState: CurrentState
}

const initialState: State = {
    currentState: {}
}

export const serverSlice = createSlice({
    name: 'servers',
    initialState,
    reducers: {
        setCurrentServer: (state, action: PayloadAction<ServerDto>) => {
            state.currentState.currentServer = action.payload;
        },
        setCurrentChannel: (state, action: PayloadAction<ChannelDto>) => {
            state.currentState.currentChannel = action.payload;
        }
    }
})

export const {
    setCurrentChannel,
    setCurrentServer} = serverSlice.actions;

export default serverSlice.reducer

/*
export type DeleteMessage = {
    channelId: string
    serverId?: string
    messageId: string
}

type CurrentState = {
    currentServer?: ServerDto,
    currentChannel?: ChannelDto
}

type State = {
    servers?: ServerState[]
    directMessages?: ChannelState[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
    currentState: CurrentState
}

export type ServerState = ServerDto & { loading?: 'idle' | 'pending' | 'succeeded' | 'failed' }

export type ChannelState = ChannelDto & { loading?: 'idle' | 'pending' | 'succeeded' | 'failed' }

export type UserState = {
    user: UserDto
    //messages?: ChannelMessageDto[]
    loading?: 'idle' | 'pending' | 'succeeded' | 'failed'
}

const serverAdapter = createEntityAdapter<ServerState>();
//const channelAdapter = createEntityAdapter<ChannelState>();
//const messageAdapter = createEntityAdapter<ChannelMessageDto>();

const initState: State = {
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
            //const serverFetch = await getV1ServersServerId(serverData[i].id!);

            const newServerState: ServerState = serverData[i];
            newServerState.loading = 'idle';

                {
                id: serverData[i].id,
                name: serverData[i].name,
                ownerUserId: serverData[i].ownerUserId;
                channels: serverData[i].channels,
                loading: 'idle',
                users: []// serverFetch.data.members
            }

            newState.servers?.push(newServerState);
        }
        //return newState;
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

        //const users = await getV1ServersServerIdUsers(serverId);
        //const userData = users.data;

        const newServerState: ServerState =  {
            channels: [],
            loading: 'succeeded',
            users: []
        }

        for(let i = 0; i < channelData.length; i++){
            const channelState: ChannelState = {
                channel: channelData[i],
                loading: 'idle',
                messages: []
            }
            newServerState.channels.push(channelState);
        }
        return newServerState;
    }
)

const initialState = serverAdapter.getInitialState();


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
          state.servers?.push(action.payload)
        },
        addChannel: (state, action: PayloadAction<ChannelState>) => {
            const serverId = action.payload.serverId;
            const server = state.servers?.find((server) => server?.id === serverId);
            //const channels = action.payload
            //state.servers?[serverId].channels?.push(action.payload)
            state.servers?[server]. .channels?.push(action.payload)
            //if(findServer)
            //    state.servers?[findServer]. .channels.push(action.payload);
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
            //const serverEntries = action.payload.
            //state. = 'pending'
        })
        builder.addCase(fetchServersAsync.fulfilled, (state, action) => {
            const serverEntries = action.payload.map(server => {
                return {id: server.id, channels: server.channels}
            })
            serverAdapter.setAll(state, serverEntries);
            //state.servers = action.payload.;
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
            const serverId = action.meta.arg;
            const channelEntry = state.entities[serverId]?.channels;
            if(channelEntry){
                serverAdapter.setAll(channelEntry.m: action.payload)
            }

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

export const {addMessageToChannel,
    addMessageToUserChannel,
    addServer,
    deleteMessageFromChannel,
    setCurrentChannel,
    setCurrentServer,
    addChannel} = serverSlice.actions;

export const selectMainState = (state: RootState) => state.servers;
export const selectCurrentState = (state: RootState) => state.servers.currentState;
export const selectServers = (state: RootState) => state.servers.servers;
export const selectServersStatus = (state: RootState) => state.servers.loading;

//export const selectChannels = (state: RootState) => state.servers.;
//export const getCurrentChannel = (state: RootState) => state.servers.currentChannel;

export default serverSlice.reducer

*/