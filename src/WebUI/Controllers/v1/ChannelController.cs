using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.v1;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class ChannelController : ControllerBase
{
    private readonly ILogger<ChannelController> _logger;
    private IChannelService _channelService;

    static readonly string[] scopeRequiredByApi = new[] { "API.Access" };
     
    public ChannelController(ILogger<ChannelController> logger, IChannelService channelService)
    {
        _logger = logger;
        _channelService = channelService;
    }   
    
    [HttpGet("channels")]
    public async Task<List<Channel>> GetChannels()
    {
        HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
        var channels = await _channelService.GetChannels();
        return channels;
    }
        
    [HttpPost("{name}")]
    public async Task<IActionResult> CreateChannel([FromRoute]string name)
    {
        HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
        var channel = new Channel
        {
            Id = Guid.NewGuid(),
            Name = name,
        };
        await _channelService.CreateChannelAsync(channel);
        return Ok(channel);
    }
    
    [HttpDelete("{channelId:guid}")]
    public async Task<IActionResult> DeleteChannelById(Guid channelId)
    {
        HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
        //var o = await _chatService.UserOwnsMessageAsync(messageId, HttpContext.GetUserId());

        //if (!ownsmessage)
        //    return BadRequest(new {error = "User does not own message"});

        var channeldeleted = await _channelService.DeleteChannelAsync(channelId);
        return channeldeleted ? NoContent() : NotFound();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateChannel([FromBody] Channel? channel)
    {
        HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
        if (channel is null)
            return BadRequest();
            
        //var ownsmessage = await _chatService.UserOwnsMessageAsync(message.Id, HttpContext.GetUserId());

        //if (!ownsmessage)
        //    return BadRequest(new {error = "User does not own message"});

        await _channelService.UpdateChannelAsync(channel);
        return Ok();
    }
}