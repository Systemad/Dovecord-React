using Infrastructure.Dtos.Channel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebUI.Domain.Channels.Features;
using WebUI.Domain.Channels.Features;
namespace WebUI.Controllers.v1;

// TODO:
//[Authorize]
[ApiController]
//[RequiredScope("API.Access")]
[Route("api/channels")]
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
    
    [HttpGet]
    public async Task<IActionResult> GetChannels()
    {
        var query = new GetChannelList.ChannelListQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetChannelById(Guid id)
    {
        var query = new GetChannel.ChannelQuery(id);
        var result = await _mediator.Send(query);
        return result != null ? Ok(result) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddChannel([FromBody] ChannelManipulationDto channelForCreation)
    {
        var command = new AddChannel.AddChannelCommand(channelForCreation);
        var commandResponse = await _mediator.Send(command);
        return Ok(commandResponse);
    }
    
    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteChannel.DeleteChannelCommand(id));
        return NoContent();
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateChannel([FromBody]UpdateChannel.UpdateChannelCommand command)
    {
        /*
        if (id != command.Id)
        {
            return BadRequest();
        }
        */

        await _mediator.Send(command);
        return NoContent();
    }
}