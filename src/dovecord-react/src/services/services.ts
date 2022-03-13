/**
 * AUTO_GENERATED Do not change this file directly, use config.ts file instead
 *
 * @version 5
 */

import { AxiosRequestConfig } from "axios";
import { SwaggerResponse } from "./config";
import { Http } from "./httpRequest";
import {
  GetWeatherforecastQueryParams,
  PutV1MessagesIdQueryParams,
  WeatherForecast,
  ChannelMessageDto,
  ChannelDto,
  ChannelManipulationDto,
  MessageManipulationDto,
  ServerDto,
  ServerManipulationDto,
  UserDto,
  UserCreationDto,
  UserManipulationDto,
} from "./types";

/* tslint:disable */
/* eslint-disable */

// eslint-disable-next-line @typescript-eslint/no-unused-vars
const __DEV__ = process.env.NODE_ENV !== "production";

// eslint-disable-next-line @typescript-eslint/no-unused-vars
function overrideConfig(
  config?: AxiosRequestConfig,
  configOverride?: AxiosRequestConfig
): AxiosRequestConfig {
  return {
    ...config,
    ...configOverride,
    headers: {
      ...config?.headers,
      ...configOverride?.headers,
    },
  };
}

// eslint-disable-next-line @typescript-eslint/no-unused-vars
export function template(path: string, obj: { [x: string]: any } = {}) {
  Object.keys(obj).forEach((key) => {
    const re = new RegExp(`{${key}}`, "i");
    path = path.replace(re, obj[key]);
  });

  return path;
}

// eslint-disable-next-line @typescript-eslint/no-unused-vars
function objToForm(requestBody: object) {
  const formData = new FormData();

  Object.entries(requestBody).forEach(([key, value]) => {
    value && formData.append(key, value);
  });

  return formData;
}

export const deleteV1ChannelsId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.deleteRequest(
    template(deleteV1ChannelsId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
deleteV1ChannelsId.key = "/api/v1/channels/{id}";

export const deleteV1MessagesId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.deleteRequest(
    template(deleteV1MessagesId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
deleteV1MessagesId.key = "/api/v1/messages/{id}";

export const deleteV1ServersId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.deleteRequest(
    template(deleteV1ServersId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
deleteV1ServersId.key = "/api/v1/servers/{id}";

export const deleteV1UsersId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.deleteRequest(
    template(deleteV1UsersId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
deleteV1UsersId.key = "/api/v1/users/{id}";

export const getV1ChannelsChannelsChannelIdMessages = (
  channelId: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<ChannelMessageDto[]>> => {
  return Http.getRequest(
    template(getV1ChannelsChannelsChannelIdMessages.key, { channelId }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getV1ChannelsChannelsChannelIdMessages.key =
  "/api/v1/channels/channels/{channelId}/messages";

export const getV1ChannelsId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<ChannelDto>> => {
  return Http.getRequest(
    template(getV1ChannelsId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getV1ChannelsId.key = "/api/v1/channels/{id}";

export const getV1MessagesChannelId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<ChannelMessageDto[]>> => {
  return Http.getRequest(
    template(getV1MessagesChannelId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getV1MessagesChannelId.key = "/api/v1/messages/channel/{id}";

export const getV1MessagesId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<ChannelMessageDto>> => {
  return Http.getRequest(
    template(getV1MessagesId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getV1MessagesId.key = "/api/v1/messages/{id}";

export const getV1Servers = (
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<ServerDto[]>> => {
  return Http.getRequest(
    getV1Servers.key,
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getV1Servers.key = "/api/v1/servers";

export const getV1ServersApiMeServers = (
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<ServerDto[]>> => {
  return Http.getRequest(
    getV1ServersApiMeServers.key,
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getV1ServersApiMeServers.key = "/api/v1/servers/api/me/servers";

export const getV1ServersServerId = (
  serverId: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<ServerDto>> => {
  return Http.getRequest(
    template(getV1ServersServerId.key, { serverId }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getV1ServersServerId.key = "/api/v1/servers/{serverId}";

export const getV1ServersServerIdChannels = (
  serverId: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<ChannelDto[]>> => {
  return Http.getRequest(
    template(getV1ServersServerIdChannels.key, { serverId }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getV1ServersServerIdChannels.key = "/api/v1/servers/{serverId}/channels";

export const getV1Users = (
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<UserDto[]>> => {
  return Http.getRequest(
    getV1Users.key,
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getV1Users.key = "/api/v1/users";

export const getV1UsersId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<UserDto>> => {
  return Http.getRequest(
    template(getV1UsersId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getV1UsersId.key = "/api/v1/users/{id}";

export const getWeatherforecast = (
  queryParams?: GetWeatherforecastQueryParams,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<WeatherForecast[]>> => {
  return Http.getRequest(
    getWeatherforecast.key,
    queryParams,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getWeatherforecast.key = "/weatherforecast";

export const postV1Channels = (
  requestBody: ChannelManipulationDto,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.postRequest(
    postV1Channels.key,
    undefined,
    requestBody,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
postV1Channels.key = "/api/v1/channels";

export const postV1Messages = (
  requestBody: MessageManipulationDto,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.postRequest(
    postV1Messages.key,
    undefined,
    requestBody,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
postV1Messages.key = "/api/v1/messages";

export const postV1Servers = (
  requestBody: ServerManipulationDto,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.postRequest(
    postV1Servers.key,
    undefined,
    requestBody,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
postV1Servers.key = "/api/v1/servers";

export const postV1ServersJoinServerId = (
  serverId: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.postRequest(
    template(postV1ServersJoinServerId.key, { serverId }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
postV1ServersJoinServerId.key = "/api/v1/servers/join/{serverId}";

export const postV1ServersLeaveServerId = (
  serverId: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.postRequest(
    template(postV1ServersLeaveServerId.key, { serverId }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
postV1ServersLeaveServerId.key = "/api/v1/servers/leave/{serverId}";

/**
 * @deprecated This endpoint deprecated and will be remove. Please use an alternative
 */
export const postV1Users = (
  requestBody: UserCreationDto,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  if (__DEV__) {
    console.warn(
      "postV1Users",
      "This endpoint deprecated and will be remove. Please use an alternative"
    );
  }
  return Http.postRequest(
    postV1Users.key,
    undefined,
    requestBody,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
postV1Users.key = "/api/v1/users";

export const putV1ChannelsId = (
  id: string,
  requestBody: ChannelManipulationDto,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.putRequest(
    template(putV1ChannelsId.key, { id }),
    undefined,
    requestBody,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
putV1ChannelsId.key = "/api/v1/channels/{id}";

export const putV1MessagesId = (
  id: string,
  queryParams?: PutV1MessagesIdQueryParams,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.putRequest(
    template(putV1MessagesId.key, { id }),
    queryParams,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
putV1MessagesId.key = "/api/v1/messages/{id}";

export const putV1ServersId = (
  id: string,
  requestBody: ServerManipulationDto,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.putRequest(
    template(putV1ServersId.key, { id }),
    undefined,
    requestBody,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
putV1ServersId.key = "/api/v1/servers/{id}";

export const putV1UsersId = (
  id: string,
  requestBody: UserManipulationDto,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.putRequest(
    template(putV1UsersId.key, { id }),
    undefined,
    requestBody,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
putV1UsersId.key = "/api/v1/users/{id}";
export const _CONSTANT0 = {
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json",
  },
};
export const _CONSTANT1 = [{ bearer: [] }];
