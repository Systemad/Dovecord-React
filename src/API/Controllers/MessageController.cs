using Application.Messages.Features;
using Domain.Channels;
using Domain.Messages;
using Domain.Messages.Dto;
using Dovecord.Extensions.Services;
using Dovecord.SignalR.Helpers;
using Dovecord.SignalR.Hubs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Web.Resource;
using Orleans;

namespace Dovecord.Controllers;

[Authorize]
[RequiredScope("API.Access")]
[ApiController]
[Route("api/v{version:apiVersion}/messages")]
[ApiVersion("1.0")]
public class MessageController : ControllerBase
{
    private readonly ILogger<MessageController> _logger;
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    private readonly IHubContext<BaseHub, IBaseHub> _hubContext;
    private readonly IClusterClient _clusterClient;

    public MessageController(ILogger<MessageController> logger,
        IMediator mediator,
        ICurrentUserService currentUserService,
        IHubContext<BaseHub, IBaseHub> hubContext, IClusterClient clusterClient)
    {
        _logger = logger;
        _mediator = mediator;
        _currentUserService = currentUserService;
        _hubContext = hubContext;
        _clusterClient = clusterClient;
    }

/*
 * public record AddMessageCommand(Guid MessageId, string Content,
    string CreatedBy, DateTime CreatedOn,
    bool IsEdit, DateTime LastModifiedOn,
    int Type, Guid ChannelId,
    Guid? ServerId, Guid AuthorId);
 */
    [ProducesResponseType(typeof(ChannelMessageDto), 201)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddMessage")]
    public IActionResult SaveMessage([FromBody] CreateMessageModel message)
    {
        var messageId = Guid.NewGuid();
        var grain = _clusterClient.GetGrain<IChannelGrain>(message.ChannelId);
        var newMsgCmd = new AddMessageCommand(messageId, message.Content,
            message.CreatedBy, message.CreatedOn,
            message.IsEdit, message.LastModifiedOn,
            message.Type, message.ChannelId,
            message.ServerId, message.AuthorId);
        grain.AddMessageAsync(newMsgCmd);
        return Ok();
    }
    
    [ProducesResponseType(204)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateMessage")]
    public async Task<IActionResult> UpdateMessage(Guid id, string message)
    {
        var command = new UpdateMessage.UpdateMessageCommand(id, message, _currentUserService.UserId);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [ProducesResponseType(typeof(IEnumerable<ChannelMessageDto>), 200)]
    [Produces("application/json")]
    [HttpGet("channel/{id:guid}", Name ="GetMessages")]
    public async Task<IActionResult> GetMessagesFromChannel(Guid id)
    {
        var command = new GetMessagesFromChannel.MessageListQuery(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [ProducesResponseType(typeof(ChannelMessageDto), 200)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetMessage")]
    public async Task<ActionResult<ChannelMessageDto>> GetMessage(Guid id)
    {
        var query = new GetMessage.MessageQuery(id);
        var queryResponse = await _mediator.Send(query);
        return Ok(queryResponse);
    }
    
    [ProducesResponseType(204)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteMessageById(Guid id)
    {
        var message = await _mediator.Send(new GetMessage.MessageQuery(id));
        await _mediator.Send(new DeleteMessage.DeleteMessageCommand(id, _currentUserService.UserId));
        await _hubContext.Clients.All.DeleteMessageReceived(message.ChannelId.ToString(), message.Id.ToString());
        return NoContent();
    }
}