import { ChannelMessageDto } from "./types";

export enum ChatOperationsNames {
  StartWorkAsync = "StartWorkAsync",
  StopWork = "StopWork",
  StopWork2 = "StopWork2",
}

export type ChatOperations = {
  [ChatOperationsNames.StartWorkAsync]: (
    message?: ChannelMessageDto
  ) => Promise<void>;
  [ChatOperationsNames.StopWork]: (
    message?: ChannelMessageDto
  ) => Promise<void>;
  [ChatOperationsNames.StopWork2]: (message?: string) => Promise<void>;
};

export enum ChatCallbacksNames {
  hello = "hello",
  MessageReceived = "MessageReceived",
  DeleteMessageReceived = "DeleteMessageReceived",
}

export type ChatCallbacks = {
  [ChatCallbacksNames.hello]: (message: string) => void;
  [ChatCallbacksNames.MessageReceived]: (message?: ChannelMessageDto) => void;
  [ChatCallbacksNames.DeleteMessageReceived]: (
    channelId?: string,
    messageId?: string,
    serverId?: string,
  ) => void;
};

export interface Chat {
  callbacksName: ChatCallbacksNames;
  callbacks: ChatCallbacks;

  methodsName: ChatOperationsNames;
  methods: ChatOperations;
}
