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
}

export interface ChannelManipulationDto {
  name?: string;
}

export interface ChannelMessageDto {
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
  userId?: string;
}

export interface MessageManipulationDto {
  /**
   *
   * - Format: guid
   */
  channelId?: string;
  content?: string;
}

export interface PrivateMessageDto {
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
  receiverUserId?: string;
  /**
   *
   * - Format: guid
   */
  userId?: string;
}

export interface PrivateMessageManipulationDto {
  content?: string;
  /**
   *
   * - Format: guid
   */
  receiverId?: string;
}

export interface PutMessagesIdQueryParams {
  message?: string;
}

export interface PutPmessagesIdQueryParams {
  message?: string;
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
