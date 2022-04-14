using Dovecord.Domain.Users.Features;
using MediatR;
using Serilog;

namespace Dovecord.Services;

public class EventQueueProcessor : BackgroundService
{
    private readonly ILogger<EventQueueProcessor> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventQueue _eventQueue;

    public EventQueueProcessor(ILogger<EventQueueProcessor> logger, IEventQueue eventQueue, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _eventQueue = eventQueue;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var queueItem = await _eventQueue.DeQueue(stoppingToken);
            Log.Information("EventQueueProcessor");
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var services = scope.ServiceProvider;
                var mediator = services.GetRequiredService<IMediator>();

                if (queueItem is IEnsureUserExistsRequest userExistsRequest)
                {
                    await mediator.Send(
                        new EnsureUserExists.EnsureUserExistCommand(userExistsRequest.UserId),
                        stoppingToken);
                }
                
                Log.Information("EventQueueProcessor: Sending command");
                await mediator.Send(queueItem, stoppingToken);
            }
            catch (Exception e)
            {
                Log.Error("Error executing queue item: {E}", e);
            }
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        Log.Information("Event queue stopping");
        await base.StopAsync(stoppingToken);
    }
}