using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// TODO: Refactor controller implementation i.e add error codes etc
namespace WebUI.Controllers.v1;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class ChatController : ControllerBase
{
    private readonly ILogger<ChatController> _logger;
    private IChatService _chatService;

    static readonly string[] scopeRequiredByApi = new[] { "API.Access" };
     
    public ChatController(ILogger<ChatController> logger, IChatService chatService)
    {
        _logger = logger;
        _chatService = chatService;
    }

    [HttpPost]
    public async Task<IActionResult> SaveMessageToChannel([FromBody] ChannelMessage? message)
    {
        HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            
        if (message is null)
            return BadRequest("Message is null");
        await _chatService.SaveMessageToChannelAsync(message);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateMessage([FromBody] ChannelMessage? message)
    {
        HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
        if (message is null)
            return BadRequest("Message is null");
            
        var ownsmessage = await _chatService.UserOwnsMessageAsync(message.Id, HttpContext.GetUserId());

        if (!ownsmessage)
            return BadRequest(new {error = "User does not own message"});
            
        await _chatService.UpdateMessageAsync(message);
        return Ok();
    }
        
    [HttpGet("{channelId:guid}")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Channel))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMessagesFromChannelId(Guid channelId)
    {
        HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
        return Ok(await _chatService.GetMessagesByChannelIdAsync(channelId));
    }
        
    [HttpDelete("{messageId:guid}")]
    public async Task<IActionResult> DeleteMessageById(Guid messageId)
    {
        HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
        var ownsmessage = await _chatService.UserOwnsMessageAsync(messageId, HttpContext.GetUserId());

        if (!ownsmessage)
            return BadRequest(new {error = "User does not own message"});
            
        var messagedeleted = await _chatService.DeleteMessageByIdAsync(messageId);
        return messagedeleted ? NoContent() : NotFound();
    }
}