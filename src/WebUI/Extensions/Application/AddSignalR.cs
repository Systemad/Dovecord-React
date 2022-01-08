namespace WebUI.Extensions.Application;

public static class AddSignalR
{
    
    public static IServiceCollection AddSignalRApplication(this IServiceCollection service)
    {
        service.AddSignalR(options => options.EnableDetailedErrors = true);
        // Since there are resolver issues from PascalCase (C# class) to CamelCase Typescript
        // and no real good solution, we will be leaving this out for now
        //.AddMessagePackProtocol();

        return service;
    }
}