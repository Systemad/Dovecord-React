import { emptySplitApi as api } from "C:/Users/yeahg/RiderProjects/Dovecord-React/src/dovecord-react/src/redux/emptyApi";
const injectedRtkApi = api.injectEndpoints({
  endpoints: (build) => ({
    weatherForecastGet: build.query<
      WeatherForecastGetApiResponse,
      WeatherForecastGetApiArg
    >({
      query: (queryArg) => ({
        url: `/weatherforecast`,
        params: { "api-version": queryArg["api-version"] },
      }),
    }),
    channelGetChannelMessages: build.query<
      ChannelGetChannelMessagesApiResponse,
      ChannelGetChannelMessagesApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/channels/channels/${queryArg.channelId}/messages`,
      }),
    }),
    channelGetChannel: build.query<
      ChannelGetChannelApiResponse,
      ChannelGetChannelApiArg
    >({
      query: (queryArg) => ({ url: `/api/v1/channels/${queryArg.id}` }),
    }),
    channelDeleteChannel: build.mutation<
      ChannelDeleteChannelApiResponse,
      ChannelDeleteChannelApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/channels/${queryArg.id}`,
        method: "DELETE",
      }),
    }),
    channelUpdateChannel: build.mutation<
      ChannelUpdateChannelApiResponse,
      ChannelUpdateChannelApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/channels/${queryArg.id}`,
        method: "PUT",
        body: queryArg.channelManipulationDto,
      }),
    }),
    channelAddChannel: build.mutation<
      ChannelAddChannelApiResponse,
      ChannelAddChannelApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/channels`,
        method: "POST",
        body: queryArg.channelManipulationDto,
      }),
    }),
    messageSaveMessage: build.mutation<
      MessageSaveMessageApiResponse,
      MessageSaveMessageApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/messages`,
        method: "POST",
        body: queryArg.messageManipulationDto,
      }),
    }),
    messageUpdateMessage: build.mutation<
      MessageUpdateMessageApiResponse,
      MessageUpdateMessageApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/messages/${queryArg.id}`,
        method: "PUT",
        params: { message: queryArg.message },
      }),
    }),
    messageGetMessage: build.query<
      MessageGetMessageApiResponse,
      MessageGetMessageApiArg
    >({
      query: (queryArg) => ({ url: `/api/v1/messages/${queryArg.id}` }),
    }),
    messageDeleteMessageById: build.mutation<
      MessageDeleteMessageByIdApiResponse,
      MessageDeleteMessageByIdApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/messages/${queryArg.id}`,
        method: "DELETE",
      }),
    }),
    messageGetMessagesFromChannel: build.query<
      MessageGetMessagesFromChannelApiResponse,
      MessageGetMessagesFromChannelApiArg
    >({
      query: (queryArg) => ({ url: `/api/v1/messages/channel/${queryArg.id}` }),
    }),
    serverGetServers: build.query<
      ServerGetServersApiResponse,
      ServerGetServersApiArg
    >({
      query: () => ({ url: `/api/v1/servers` }),
    }),
    serverAddServer: build.mutation<
      ServerAddServerApiResponse,
      ServerAddServerApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/servers`,
        method: "POST",
        body: queryArg.serverManipulationDto,
      }),
    }),
    serverGetServersOfUser: build.query<
      ServerGetServersOfUserApiResponse,
      ServerGetServersOfUserApiArg
    >({
      query: () => ({ url: `/api/v1/servers/api/me/servers` }),
    }),
    serverGetServerById: build.query<
      ServerGetServerByIdApiResponse,
      ServerGetServerByIdApiArg
    >({
      query: (queryArg) => ({ url: `/api/v1/servers/${queryArg.serverId}` }),
    }),
    serverGetChannels: build.query<
      ServerGetChannelsApiResponse,
      ServerGetChannelsApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/servers/${queryArg.serverId}/channels`,
      }),
    }),
    serverDeleteServer: build.mutation<
      ServerDeleteServerApiResponse,
      ServerDeleteServerApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/servers/${queryArg.id}`,
        method: "DELETE",
      }),
    }),
    serverUpdateServer: build.mutation<
      ServerUpdateServerApiResponse,
      ServerUpdateServerApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/servers/${queryArg.id}`,
        method: "PUT",
        body: queryArg.serverManipulationDto,
      }),
    }),
    serverJoinServer: build.mutation<
      ServerJoinServerApiResponse,
      ServerJoinServerApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/servers/join/${queryArg.serverId}`,
        method: "POST",
      }),
    }),
    serverLeaveServer: build.mutation<
      ServerLeaveServerApiResponse,
      ServerLeaveServerApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/servers/leave/${queryArg.serverId}`,
        method: "POST",
      }),
    }),
    userGetUsers: build.query<UserGetUsersApiResponse, UserGetUsersApiArg>({
      query: () => ({ url: `/api/v1/users` }),
    }),
    userAddUser: build.mutation<UserAddUserApiResponse, UserAddUserApiArg>({
      query: (queryArg) => ({
        url: `/api/v1/users`,
        method: "POST",
        body: queryArg.userCreationDto,
      }),
    }),
    userGetUser: build.query<UserGetUserApiResponse, UserGetUserApiArg>({
      query: (queryArg) => ({ url: `/api/v1/users/${queryArg.id}` }),
    }),
    userDeleteUser: build.mutation<
      UserDeleteUserApiResponse,
      UserDeleteUserApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/users/${queryArg.id}`,
        method: "DELETE",
      }),
    }),
    userUpdateUser: build.mutation<
      UserUpdateUserApiResponse,
      UserUpdateUserApiArg
    >({
      query: (queryArg) => ({
        url: `/api/v1/users/${queryArg.id}`,
        method: "PUT",
        body: queryArg.userManipulationDto,
      }),
    }),
  }),
  overrideExisting: false,
});
export { injectedRtkApi as webApi };
export type WeatherForecastGetApiResponse =
  /** status 200  */ WeatherForecast[];
export type WeatherForecastGetApiArg = {
  "api-version"?: string | null;
};
export type ChannelGetChannelMessagesApiResponse =
  /** status 200  */ ChannelMessageDto[];
export type ChannelGetChannelMessagesApiArg = {
  channelId: string;
};
export type ChannelGetChannelApiResponse = /** status 200  */ ChannelDto;
export type ChannelGetChannelApiArg = {
  id: string;
};
export type ChannelDeleteChannelApiResponse = unknown;
export type ChannelDeleteChannelApiArg = {
  id: string;
};
export type ChannelUpdateChannelApiResponse = unknown;
export type ChannelUpdateChannelApiArg = {
  id: string;
  channelManipulationDto: ChannelManipulationDto;
};
export type ChannelAddChannelApiResponse = /** status 201  */ ChannelDto;
export type ChannelAddChannelApiArg = {
  channelManipulationDto: ChannelManipulationDto;
};
export type MessageSaveMessageApiResponse =
  /** status 201  */ ChannelMessageDto;
export type MessageSaveMessageApiArg = {
  messageManipulationDto: MessageManipulationDto;
};
export type MessageUpdateMessageApiResponse = unknown;
export type MessageUpdateMessageApiArg = {
  id: string;
  message?: string | null;
};
export type MessageGetMessageApiResponse = /** status 200  */ ChannelMessageDto;
export type MessageGetMessageApiArg = {
  id: string;
};
export type MessageDeleteMessageByIdApiResponse = unknown;
export type MessageDeleteMessageByIdApiArg = {
  id: string;
};
export type MessageGetMessagesFromChannelApiResponse =
  /** status 200  */ ChannelMessageDto[];
export type MessageGetMessagesFromChannelApiArg = {
  id: string;
};
export type ServerGetServersApiResponse = /** status 200  */ ServerDto[];
export type ServerGetServersApiArg = void;
export type ServerAddServerApiResponse = /** status 201  */ ServerDto;
export type ServerAddServerApiArg = {
  serverManipulationDto: ServerManipulationDto;
};
export type ServerGetServersOfUserApiResponse = /** status 200  */ ServerDto[];
export type ServerGetServersOfUserApiArg = void;
export type ServerGetServerByIdApiResponse = /** status 200  */ ServerDto;
export type ServerGetServerByIdApiArg = {
  serverId: string;
};
export type ServerGetChannelsApiResponse = /** status 200  */ ChannelDto[];
export type ServerGetChannelsApiArg = {
  serverId: string;
};
export type ServerDeleteServerApiResponse = unknown;
export type ServerDeleteServerApiArg = {
  id: string;
};
export type ServerUpdateServerApiResponse = unknown;
export type ServerUpdateServerApiArg = {
  id: string;
  serverManipulationDto: ServerManipulationDto;
};
export type ServerJoinServerApiResponse = unknown;
export type ServerJoinServerApiArg = {
  serverId: string;
};
export type ServerLeaveServerApiResponse = unknown;
export type ServerLeaveServerApiArg = {
  serverId: string;
};
export type UserGetUsersApiResponse = /** status 200  */ UserDto[];
export type UserGetUsersApiArg = void;
export type UserAddUserApiResponse = /** status 201  */ UserDto;
export type UserAddUserApiArg = {
  userCreationDto: UserCreationDto;
};
export type UserGetUserApiResponse = /** status 200  */ UserDto;
export type UserGetUserApiArg = {
  id: string;
};
export type UserDeleteUserApiResponse = unknown;
export type UserDeleteUserApiArg = {
  id: string;
};
export type UserUpdateUserApiResponse = unknown;
export type UserUpdateUserApiArg = {
  id: string;
  userManipulationDto: UserManipulationDto;
};
export type WeatherForecast = {
  date?: string;
  temperatureC?: number;
  temperatureF?: number;
  summary?: string | null;
};
export type ChannelMessage = {
  id?: string;
  content?: string | null;
  createdBy?: string | null;
  createdOn?: string;
  isEdit?: boolean;
  lastModifiedOn?: string | null;
  channelId?: string;
  channel?: Channel;
  serverId?: string | null;
  server?: Server | null;
  authorId?: string | null;
  author?: User | null;
};
export type Channel = {
  id?: string;
  type?: number;
  name?: string | null;
  topic?: string | null;
  serverId?: string | null;
  server?: Server | null;
  messages?: ChannelMessage[] | null;
  recipients?: User[] | null;
};
export type Server = {
  id?: string;
  name?: string;
  iconUrl?: string | null;
  ownerUserId?: string;
  channels?: Channel[] | null;
  members?: User[] | null;
};
export type User = {
  id?: string;
  username?: string | null;
  isOnline?: boolean | null;
  bot?: boolean | null;
  system?: boolean | null;
  accentColor?: boolean | null;
  servers?: Server[];
};
export type ChannelMessageDto = {
  id?: string;
  createdOn?: string;
  createdBy?: string | null;
  isEdit?: boolean;
  lastModifiedOn?: string | null;
  content?: string | null;
  author?: User;
  channelId?: string;
  serverId?: string | null;
};
export type ChannelDto = {
  id?: string;
  type?: number;
  name?: string | null;
  topic?: string | null;
  serverId?: string | null;
  recipients?: User[] | null;
};
export type ChannelManipulationDto = {
  name?: string | null;
  type?: number | null;
  topic?: string | null;
  serverId?: string | null;
};
export type MessageManipulationDto = {
  content?: string | null;
  channelId?: string;
};
export type ServerDto = {
  id?: string;
  name?: string | null;
  topic?: string | null;
  ownerUserId?: string;
  channels?: Channel[] | null;
  members?: User[] | null;
};
export type ServerManipulationDto = {
  name?: string | null;
};
export type UserDto = {
  id?: string;
  name?: string | null;
  isOnline?: boolean | null;
};
export type UserCreationDto = {
  name?: string | null;
  isOnline?: boolean | null;
};
export type UserManipulationDto = {
  isOnline?: boolean | null;
};
export const {
  useWeatherForecastGetQuery,
  useChannelGetChannelMessagesQuery,
  useChannelGetChannelQuery,
  useChannelDeleteChannelMutation,
  useChannelUpdateChannelMutation,
  useChannelAddChannelMutation,
  useMessageSaveMessageMutation,
  useMessageUpdateMessageMutation,
  useMessageGetMessageQuery,
  useMessageDeleteMessageByIdMutation,
  useMessageGetMessagesFromChannelQuery,
  useServerGetServersQuery,
  useServerAddServerMutation,
  useServerGetServersOfUserQuery,
  useServerGetServerByIdQuery,
  useServerGetChannelsQuery,
  useServerDeleteServerMutation,
  useServerUpdateServerMutation,
  useServerJoinServerMutation,
  useServerLeaveServerMutation,
  useUserGetUsersQuery,
  useUserAddUserMutation,
  useUserGetUserQuery,
  useUserDeleteUserMutation,
  useUserUpdateUserMutation,
} = injectedRtkApi;