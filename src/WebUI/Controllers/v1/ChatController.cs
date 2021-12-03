using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebUI.Domain.Messages.Features;

// TODO: Refactor controller implementation i.e add error codes etc
namespace WebUI.Controllers.v1;

//[Authorize]
[ApiController]
//[RequiredScope("API.Access")]
[Route("api/messages")]
[ApiVersion("1.0")]
public class ChatController : ControllerBase
{
    private readonly ILogger<ChatController> _logger;
    private readonly IMediator _mediator;

    static readonly string[] scopeRequiredByApi = new[] { "API.Access" };
     
    public ChatController(ILogger<ChatController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SaveMessageToChannel([FromBody] Create.Command message)
    {
        await _mediator.Send(message);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateMessage([FromBody] Edit.UpdateChannelCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
        
    [HttpGet("{channelId:guid}")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Channel))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMessagesFromChannelId(Guid channelId)
    {
        var query = new List.Query(channelId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
        
    [HttpDelete("{messageId:guid}")]
    public async Task<IActionResult> DeleteMessageById(Guid messageId)
    {
        await _mediator.Send(new DeleteMessage.DeleteMessageCommand(messageId));
        return NoContent();
    }
}