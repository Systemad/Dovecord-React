/**
 * AUTO_GENERATED Do not change this file directly, use config.ts file instead
 *
 * @version 5
 */

/* tslint:disable */
/* eslint-disable */

export interface Channel {
  /**
   *
   * - Format: guid
   */
  id?: string;
  messages?: ChannelMessage[];
  name?: string;
  recipients?: User[];
  server?: Server;
  /**
   *
   * - Format: guid
   */
  serverId?: string;
  topic?: string;
  /**
   *
   * - Format: int32
   */
  type?: number;
}

export interface ChannelDto {
  /**
   *
   * - Format: guid
   */
  id?: string;
  name?: string;
  recipients?: User[];
  /**
   *
   * - Format: guid
   */
  serverId?: string;
  topic?: string;
  /**
   *
   * - Format: int32
   */
  type?: number;
}

export interface ChannelManipulationDto {
  name?: string;
  /**
   *
   * - Format: guid
   */
  serverId?: string;
  topic?: string;
  /**
   *
   * - Format: int32
   */
  type?: number;
}

export interface ChannelMessage {
  author?: User;
  /**
   *
   * - Format: guid
   */
  authorId?: string;
  channel?: Channel;
  /**
   *
   * - Format: guid
   */
  channelId?: string;
  content?: string;
  createdBy?: string;
  /**
   *
   * - Format: date-time
   */
  createdOn?: string;
  /**
   *
   * - Format: guid
   */
  id?: string;
  isEdit?: boolean;
  /**
   *
   * - Format: date-time
   */
  lastModifiedOn?: string;
  server?: Server;
  /**
   *
   * - Format: guid
   */
  serverId?: string;
}

export interface ChannelMessageDto {
  author?: User;
  /**
   *
   * - Format: guid
   */
  channelId?: string;
  content?: string;
  createdBy?: string;
  /**
   *
   * - Format: date-time
   */
  createdOn?: string;
  /**
   *
   * - Format: guid
   */
  id?: string;
  isEdit?: boolean;
  /**
   *
   * - Format: date-time
   */
  lastModifiedOn?: string;
  /**
   *
   * - Format: guid
   */
  serverId?: string;
}

export interface GetWeatherforecastQueryParams {
  "api-version"?: string;
}

export interface MessageManipulationDto {
  /**
   *
   * - Format: guid
   */
  channelId?: string;
  content?: string;
}

export interface PutV1MessagesIdQueryParams {
  message?: string;
}

export interface Server {
  channels?: Channel[];
  iconUrl?: string;
  /**
   *
   * - Format: guid
   */
  id?: string;
  members?: User[];
  name?: string;
  /**
   *
   * - Format: guid
   */
  ownerUserId?: string;
}

export interface ServerDto {
  channels?: Channel[];
  /**
   *
   * - Format: guid
   */
  id?: string;
  members?: User[];
  name?: string;
  /**
   *
   * - Format: guid
   */
  ownerUserId?: string;
  topic?: string;
}

export interface ServerManipulationDto {
  name?: string;
}

export interface User {
  accentColor?: boolean;
  bot?: boolean;
  /**
   *
   * - Format: guid
   */
  id?: string;
  isOnline?: boolean;
  servers?: Server[];
  system?: boolean;
  username?: string;
}

export interface UserCreationDto {
  isOnline?: boolean;
  name?: string;
}

export interface UserDto {
  /**
   *
   * - Format: guid
   */
  id?: string;
  isOnline?: boolean;
  name?: string;
}

export interface UserManipulationDto {
  isOnline?: boolean;
}

export interface WeatherForecast {
  /**
   *
   * - Format: date-time
   */
  date?: string;
  summary?: string;
  /**
   *
   * - Format: int32
   */
  temperatureC?: number;
  /**
   *
   * - Format: int32
   */
  temperatureF?: number;
}
