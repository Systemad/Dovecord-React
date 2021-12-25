using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using WebUI.Domain.Messages.Features;
using WebUI.Dtos.Message;
using WebUI.Extensions.Services;

// TODO: Refactor controller implementation i.e add error codes etc
namespace WebUI.Controllers.v1;

[Authorize]
[RequiredScope("API.Access")]
[ApiController]
[Route("api/messages")]
[ApiVersion("1.0")]
public class MessageController : ControllerBase
{
    private readonly ILogger<MessageController> _logger;
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    static readonly string[] scopeRequiredByApi = new[] { "API.Access" };
     
    public MessageController(ILogger<MessageController> logger, IMediator mediator, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [ProducesResponseType(typeof(ChannelMessageDto), 201)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddMessage")]
    public async Task<IActionResult> SaveMessage([FromBody] MessageManipulationDto message)
    {
        var userids = _currentUserService.UserId;
        var command = new AddMessage.AddMessageCommand(message);
        var commandResponse = await _mediator.Send(command);
        return Ok(commandResponse);
    }
    
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateMessage")]
    public async Task<IActionResult> UpdateMessage(Guid id, MessageManipulationDto message)
    {
        var command = new UpdateMessage.UpdateMessageCommand(id, message);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [ProducesResponseType(typeof(IEnumerable<ChannelMessageDto>), 200)]
    [Produces("application/json")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMessagesFromChannel(Guid id)
    {
        var command = new GetMessageList.MessageListQuery(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
        
    [Produces("application/json")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMessageById(Guid id)
    {
        await _mediator.Send(new DeleteMessage.DeleteMessageCommand(id));
        return NoContent();
    }
}