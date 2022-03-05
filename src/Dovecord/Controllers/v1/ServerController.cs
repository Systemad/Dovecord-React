using Dovecord.Domain.Channels.Dto;
using Dovecord.Domain.Channels.Features;
using Dovecord.Domain.Servers;
using Dovecord.Domain.Servers.Dto;
using Dovecord.Domain.Servers.Features;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Dovecord.Controllers.v1;

//[Authorize]
//[RequiredScope("API.Access")]
[ApiController]
[Route("api/servers")]
[ApiVersion("1.0")]
public class ServerController : ControllerBase
{
    private readonly ILogger<ServerController> _logger;
    private readonly IMediator _mediator;
    
    public ServerController(ILogger<ServerController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }   

    [ProducesResponseType(typeof(IEnumerable<ServerDto>), 200)]
    [Produces("application/json")]
    [HttpGet(Name = "GetServers")]
    public async Task<IActionResult> GetServers()
    {
        var query = new GetServerList.ServerListQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [ProducesResponseType(typeof(ServerDto), 200)]
    [Produces("application/json")]
    [HttpGet("/api/me/server", Name = "GetServersOfUser")]
    public async Task<IActionResult> GetServersOfUser()
    {
        var query = new GetServersOfUser.GetServersOfUserQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [ProducesResponseType(typeof(ServerDto), 200)]
    [Produces("application/json")]
    [HttpGet("{serverId:guid}", Name = "GetServerById")]
    public async Task<IActionResult> GetServerById(Guid serverId)
    {
        var query = new GetServerById.GetServerByIdGetQuery(serverId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [ProducesResponseType(typeof(ServerDto), 201)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddServer")]
    public async Task<IActionResult> AddServer([FromBody] ServerManipulationDto serverForCreation)
    {
        var command = new AddServer.AddServerCommand(serverForCreation);
        var commandResponse = await _mediator.Send(command);
        return Ok(commandResponse);
        //return CreatedAtAction(nameof(GetServerById), new {commandResponse.Id}, commandResponse);
    }
    
    [ProducesResponseType(204)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteServer")]
    public async Task<ActionResult> DeleteServer(Guid id)
    {
        var command = new DeleteServer.DeleteServerCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [ProducesResponseType(204)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateServer")]
    public async Task<IActionResult> UpdateServer(Guid id, ServerManipulationDto server)
    {
        var command = new UpdateServer.UpdateServerCommand(id, server);
        await _mediator.Send(command);
        return NoContent();
    }
    [ProducesResponseType(204)]
    [Produces("application/json")]
    [HttpPost("join/{serverId:guid}", Name = "JoinServer")]
    public async Task<IActionResult> JoinServer(Guid serverId)
    {
        var command = new JoinServer.JoinServerCommand(serverId);
        await _mediator.Send(command);
        return NoContent();
    }
}