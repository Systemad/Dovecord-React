namespace FunctionalTests;

public class ApiRoutes
{
    public const string Base = "api";

    public static class Channels
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/channels";
        public const string Get = $"{Base}/channels/{Id}";
        public const string Create = $"{Base}/channels";
        public const string Delete = $"{Base}/channels/{Id}";
        public const string Update = $"{Base}/channels/{Id}";
    }
    public static class Messages
    {
        public const string Id = "{id}";
        //public const string GetList = $"{Base}/messages";
        public const string GetByChannelId = $"{Base}/messages/{Id}";
        public const string Create = $"{Base}/messages";
        public const string Delete = $"{Base}/messages/{Id}";
        public const string Update = $"{Base}/messages/{Id}";
    }
    public static class Users
    {
        public const string Id = "{id}";
        //public const string GetList = $"{Base}/messages";
        public const string GetByUserId = $"{Base}/users/{Id}";
        public const string Create = $"{Base}/users";
        public const string Delete = $"{Base}/users/{Id}";
        public const string Update = $"{Base}/users/{Id}";
    }
}