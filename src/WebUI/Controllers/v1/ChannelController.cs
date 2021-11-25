using Application.Features.Channels;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.v1;

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
        var query = new List.Query();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetChannelById(Guid id)
    {
        var query = new Details.Query(id);
        var result = await _mediator.Send(query);
        return result != null ? Ok(result) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Create.Command textChannel)
    {
        var response = await _mediator.Send(textChannel);
        return Ok(response);
    }
    
    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new Delete.Command(id));
        return NoContent();
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateChannel([FromBody]Edit.Model command)
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