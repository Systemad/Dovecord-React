using Dovecord.Domain.Messages.Dto;
using Dovecord.Domain.Messages.Features;
using Dovecord.Extensions.Services;
using Dovecord.SignalR;
using Dovecord.SignalR.Hubs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Web.Resource;

namespace Dovecord.Controllers.v1;

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

    public MessageController(ILogger<MessageController> logger,
        IMediator mediator,
        ICurrentUserService currentUserService,
        IHubContext<BaseHub, IBaseHub> hubContext)
    {
        _logger = logger;
        _mediator = mediator;
        _currentUserService = currentUserService;
        _hubContext = hubContext;
    }

    [ProducesResponseType(typeof(ChannelMessageDto), 201)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddMessage")]
    public async Task<ActionResult<ChannelMessageDto>> SaveMessage([FromBody] MessageManipulationDto message)
    {
        var command = new AddMessage.AddMessageCommand(message);
        var commandResponse = await _mediator.Send(command);
        // TODO: Extract?
        // Click on Server, Users joined group by ID
        // Send message to that server id??
        await _hubContext.Clients.All.MessageReceived(commandResponse);
        //await _hubContext.Clients.Group(message.ChannelId.ToString()).MessageReceived(commandResponse);
        return CreatedAtAction(nameof(GetMessage), new {commandResponse.Id}, commandResponse);
    }
    
    [ProducesResponseType(204)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateMessage")]
    public async Task<IActionResult> UpdateMessage(Guid id, string message)
    {
        var command = new UpdateMessage.UpdateMessageCommand(id, message);
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
        await _mediator.Send(new DeleteMessage.DeleteMessageCommand(id));
        await _hubContext.Clients.All.DeleteMessageReceived(message.ChannelId.ToString(), message.Id.ToString());
        return NoContent();
    }
}