using Application.Channels.Queries;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace WebUI.Controllers.v1;

[Authorize]
[ApiController]
[RequiredScope("API.Access")]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class ChannelController : ControllerBase
{
    private readonly ILogger<ChannelController> _logger;
    private IChannelService _channelService;
    private readonly IMediator _mediator;
    
    public ChannelController(ILogger<ChannelController> logger, IMediator mediator, IChannelService channelService)
    {
        _logger = logger;
        _mediator = mediator;
        _channelService = channelService;
    }   
    
    [HttpGet("channels")]
    public async Task<IActionResult> GetChannels()
    {
        var query = new GetChannelsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("channels/{id:guid}")]
    public async Task<IActionResult> GetChannelById(Guid id)
    {
        var query = new GetChannelByIdQuery(id);
        var result = await _mediator.Send(query);
        return result != null ? Ok(result) : NotFound();
    }
        
    [HttpPost("{name}")]
    public async Task<IActionResult> CreateChannel([FromRoute]string name)
    {
        var channel = new TextChannel
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
        //var o = await _chatService.UserOwnsMessageAsync(messageId, HttpContext.GetUserId());

        //if (!ownsmessage)
        //    return BadRequest(new {error = "User does not own message"});

        var channeldeleted = await _channelService.DeleteChannelAsync(channelId);
        return channeldeleted ? NoContent() : NotFound();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateChannel([FromBody] TextChannel? channel)
    {
        if (channel is null)
            return BadRequest();
            
        //var ownsmessage = await _chatService.UserOwnsMessageAsync(message.Id, HttpContext.GetUserId());

        //if (!ownsmessage)
        //    return BadRequest(new {error = "User does not own message"});

        await _channelService.UpdateChannelAsync(channel);
        return Ok();
    }
}