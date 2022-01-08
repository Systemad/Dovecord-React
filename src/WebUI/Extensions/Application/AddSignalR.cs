namespace WebUI.Extensions.Application;

public static class AddSignalR
{
    
    public static IServiceCollection AddSignalRApplication(this IServiceCollection service)
    {
        service.AddSignalR(options => options.EnableDetailedErrors = true);
            //.AddMessagePackProtocol();

        return service;
    }
}