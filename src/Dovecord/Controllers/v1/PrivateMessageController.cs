using Dovecord.Domain.Messages.Features;
using Dovecord.Domain.PrivateMessage.Features;
using Dovecord.Dtos.Message;
using Dovecord.Dtos.PrivateMessage;
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
[Route("api/pmessages")]
[ApiVersion("1.0")]
public class PrivateMessageController : ControllerBase
{
    private readonly ILogger<PrivateMessageController> _logger;
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    private readonly IHubContext<ChatHub, IChatClient> _hubContext;

    public PrivateMessageController(ILogger<PrivateMessageController> logger,
        IMediator mediator,
        ICurrentUserService currentUserService,
        IHubContext<ChatHub, IChatClient> hubContext)
    {
        _logger = logger;
        _mediator = mediator;
        _currentUserService = currentUserService;
        _hubContext = hubContext;
    }

    [ProducesResponseType(typeof(PrivateMessageDto), 201)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddPrivateMessage")]
    public async Task<ActionResult<PrivateMessageDto>> SavePrivateMessage([FromBody] PrivateMessageManipulationDto message)
    {
        var command = new AddPrivateMessage.AddPrivateMessageCommand(message);
        var commandResponse = await _mediator.Send(command);
        await _hubContext.Clients.Clients(message.ReceiverId.ToString(), _currentUserService.UserId).PrivateMessageReceived(commandResponse);
        //await _hubContext.Clients.Group(message.ChannelId.ToString()).MessageReceived(commandResponse);
        return CreatedAtAction(nameof(GetPrivateMessage), new {commandResponse.Id}, commandResponse);
    }
    
    [ProducesResponseType(204)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdatePrivateMessage")]
    public async Task<IActionResult> UpdatePrivateMessage(Guid id, string message)
    {
        var command = new UpdatePrivateMessage.UpdatePrivateMessageCommand(id, message);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [ProducesResponseType(typeof(IEnumerable<PrivateMessageDto>), 200)]
    [Produces("application/json")]
    [HttpGet("user/{id:guid}", Name ="GetPrivateMessages")]
    public async Task<IActionResult> GetMessagesFromUser(Guid id)
    {
        var command = new GetPrivateMessageList.PrivateMessageListQuery(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [ProducesResponseType(typeof(PrivateMessageDto), 200)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetPrivateMessage")]
    public async Task<ActionResult<PrivateMessageDto>> GetPrivateMessage(Guid id)
    {
        var query = new GetPrivateMessage.PrivateMessageQuery(id);
        var queryResponse = await _mediator.Send(query);
        return Ok(queryResponse);
    }
    
    [ProducesResponseType(204)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeletePrivateMessageById(Guid id)
    {
        var message = await _mediator.Send(new GetPrivateMessage.PrivateMessageQuery(id));
        await _mediator.Send(new DeletePrivateMessage.DeletePrivateMessageCommand(id));
        await _hubContext.Clients.Clients(message.ReceiverUserId.ToString(), message.UserId.ToString()).DeleteMessageReceived(message.ReceiverUserId.ToString(), message.Id.ToString());
        return NoContent();
    }
}