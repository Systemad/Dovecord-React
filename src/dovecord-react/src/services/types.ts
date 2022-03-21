/**
 * AUTO_GENERATED Do not change this file directly, use config.ts file instead
 *
 * @version 5
 */

export interface ChannelDto {
  /**
   *
   * - Format: guid
   */
  id?: string;
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

export interface ChannelManipulationDto {
  name?: string;
  topic?: string;
  /**
   *
   * - Format: int32
   */
  type?: number;
}

export interface ChannelMessageDto {
  author?: UserDto;
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

export interface ServerDto {
  channels?: ChannelDto[];
  iconUrl?: string;
  /**
   *
   * - Format: guid
   */
  id?: string;
  members?: UserDto[];
  name?: string;
  /**
   *
   * - Format: guid
   */
  ownerUserId?: string;
}

export interface ServerManipulationDto {
  name?: string;
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
