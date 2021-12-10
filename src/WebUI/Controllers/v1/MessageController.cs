using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebUI.Domain.Messages.Features;
using WebUI.Dtos.Message;

// TODO: Refactor controller implementation i.e add error codes etc
namespace WebUI.Controllers.v1;

//[Authorize]
[ApiController]
//[RequiredScope("API.Access")]
[Route("api/messages")]
[ApiVersion("1.0")]
public class MessageController : ControllerBase
{
    private readonly ILogger<MessageController> _logger;
    private readonly IMediator _mediator;

    static readonly string[] scopeRequiredByApi = new[] { "API.Access" };
     
    public MessageController(ILogger<MessageController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost(Name = "AddMessage")]
    public async Task<IActionResult> SaveMessage([FromBody] MessageManipulationDto message)
    {
        var command = new AddMessage.AddMessageCommand(message);
        var commandResponse = await _mediator.Send(command);
        return Ok(commandResponse);
    }
    
    [HttpPut("{id:guid}", Name = "UpdateMessage")]
    public async Task<IActionResult> UpdateMessage(Guid id, MessageManipulationDto message)
    {
        var command = new UpdateMessage.UpdateMessageCommand(id, message);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [Produces("application/json")]
    [HttpGet("{id:guid}")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Channel))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMessagesFromChannel(Guid id)
    {
        var command = new GetMessageList.MessageListQuery(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
        
    [HttpDelete("{messageId:guid}")]
    public async Task<IActionResult> DeleteMessageById(Guid messageId)
    {
        await _mediator.Send(new DeleteMessage.DeleteMessageCommand(messageId));
        return NoContent();
    }
}