using Application.Channels.Features;
using Application.Messages.Features;
using Domain.Channels.Dto;
using Domain.Messages.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Dovecord.Controllers;

[Authorize]
[RequiredScope("API.Access")]
[ApiController]
[Route("api/v{version:apiVersion}/channels")]
[ApiVersion("1.0")]
public class ChannelController : ControllerBase
{
    private readonly ILogger<ChannelController> _logger;
    private readonly IMediator _mediator;
    
    public ChannelController(ILogger<ChannelController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }   
    
    [ProducesResponseType(typeof(IEnumerable<ChannelMessageDto>), 200)]
    [Produces("application/json")]
    [HttpGet("{channelId:guid}/messages", Name = "GetChannelsMessages")]
    public async Task<IActionResult> GetChannelMessages(Guid channelId)
    {
        var command = new GetMessagesFromChannel.MessageListQuery(channelId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [ProducesResponseType(typeof(ChannelDto), 200)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetChannel")]
    public async Task<IActionResult> GetChannel(Guid id)
    {
        var query = new GetChannel.ChannelQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    /*
    [ProducesResponseType(typeof(ChannelDto), 201)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddChannel")]
    public async Task<IActionResult> AddChannel([FromBody] ChannelManipulationDto channelForCreation)
    {
        var command = new AddServerChannel.AddServerChannelCommand(channelForCreation);
        var commandResponse = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetChannel), new {commandResponse.Id}, commandResponse);
    }
    */
    
    [ProducesResponseType(204)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteChannel")]
    public async Task<ActionResult> DeleteChannel(Guid id)
    {
        var command = new DeleteChannel.DeleteChannelCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [ProducesResponseType(204)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateChannel")]
    public async Task<IActionResult> UpdateChannel(Guid id, ChannelManipulationDto channel)
    {
        var command = new UpdateChannel.UpdateChannelCommand(id, channel);
        await _mediator.Send(command);
        return NoContent();
    }
}