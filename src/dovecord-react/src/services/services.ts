/**
 * AUTO_GENERATED Do not change this file directly, use config.ts file instead
 *
 * @version 5
 */

import { AxiosRequestConfig } from "axios";
import { SwaggerResponse } from "./config";
import { Http } from "./httpRequest";
import {
  PutMessagesIdQueryParams,
  WeatherForecast,
  ChannelDto,
  ChannelManipulationDto,
  ChannelMessageDto,
  MessageManipulationDto,
  UserDto,
  UserCreationDto,
  UserManipulationDto,
} from "./types";

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

export const deleteChannelsId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.deleteRequest(
    template(deleteChannelsId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
deleteChannelsId.key = "/api/channels/{id}";

export const deleteMessagesId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.deleteRequest(
    template(deleteMessagesId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
deleteMessagesId.key = "/api/messages/{id}";

export const deleteUsersId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.deleteRequest(
    template(deleteUsersId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
deleteUsersId.key = "/api/users/{id}";

export const getChannels = (
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<ChannelDto[]>> => {
  return Http.getRequest(
    getChannels.key,
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getChannels.key = "/api/channels";

export const getChannelsId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<ChannelDto>> => {
  return Http.getRequest(
    template(getChannelsId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getChannelsId.key = "/api/channels/{id}";

export const getMessagesChannelId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<ChannelMessageDto[]>> => {
  return Http.getRequest(
    template(getMessagesChannelId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getMessagesChannelId.key = "/api/messages/channel/{id}";

export const getMessagesId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<ChannelMessageDto>> => {
  return Http.getRequest(
    template(getMessagesId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getMessagesId.key = "/api/messages/{id}";

export const getUsers = (
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<UserDto[]>> => {
  return Http.getRequest(
    getUsers.key,
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getUsers.key = "/api/users";

export const getUsersId = (
  id: string,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<UserDto>> => {
  return Http.getRequest(
    template(getUsersId.key, { id }),
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getUsersId.key = "/api/users/{id}";

export const getWeatherforecast = (
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<WeatherForecast[]>> => {
  return Http.getRequest(
    getWeatherforecast.key,
    undefined,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
getWeatherforecast.key = "/weatherforecast";

export const postChannels = (
  requestBody: ChannelManipulationDto,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.postRequest(
    postChannels.key,
    undefined,
    requestBody,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
postChannels.key = "/api/channels";

export const postMessages = (
  requestBody: MessageManipulationDto,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.postRequest(
    postMessages.key,
    undefined,
    requestBody,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
postMessages.key = "/api/messages";

/**
 * @deprecated This endpoint deprecated and will be remove. Please use an alternative
 */
export const postUsers = (
  requestBody: UserCreationDto,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  if (__DEV__) {
    console.warn(
      "postUsers",
      "This endpoint deprecated and will be remove. Please use an alternative"
    );
  }
  return Http.postRequest(
    postUsers.key,
    undefined,
    requestBody,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
postUsers.key = "/api/users";

export const putChannelsId = (
  id: string,
  requestBody: ChannelManipulationDto,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.putRequest(
    template(putChannelsId.key, { id }),
    undefined,
    requestBody,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
putChannelsId.key = "/api/channels/{id}";

export const putMessagesId = (
  id: string,
  queryParams?: PutMessagesIdQueryParams,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.putRequest(
    template(putMessagesId.key, { id }),
    queryParams,
    undefined,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
putMessagesId.key = "/api/messages/{id}";

export const putUsersId = (
  id: string,
  requestBody: UserManipulationDto,
  configOverride?: AxiosRequestConfig
): Promise<SwaggerResponse<any>> => {
  return Http.putRequest(
    template(putUsersId.key, { id }),
    undefined,
    requestBody,
    _CONSTANT1,
    overrideConfig(_CONSTANT0, configOverride)
  );
};

/** Key is end point string without base url */
putUsersId.key = "/api/users/{id}";
export const _CONSTANT0 = {
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json",
  },
};
export const _CONSTANT1 = [{ JWT: [] }];
