using Application.Channels.Features;
using Application.Servers.Features;
using Domain.Channels;
using Domain.Channels.Dto;
using Domain.Servers;
using Domain.Servers.Dto;
using Domain.Users.Dto;
using Dovecord.Extensions.Services;
using Dovecord.SignalR.Helpers;
using Dovecord.SignalR.Hubs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Web.Resource;
using Orleans;

namespace Dovecord.Controllers;

[Authorize]
[RequiredScope("API.Access")]
[ApiController]
[Route("api/v{version:apiVersion}/servers")]
[ApiVersion("1.0")]
public class ServerController : ControllerBase
{
    private readonly ILogger<ServerController> _logger;
    private readonly IMediator _mediator;
    private readonly IHubContext<BaseHub, IBaseHub> _hubContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IClusterClient _client;
    
    public ServerController(ILogger<ServerController> logger, IMediator mediator, IHubContext<BaseHub, IBaseHub> hubContext, ICurrentUserService currentUserService, IClusterClient client)
    {
        _logger = logger;
        _mediator = mediator;
        _hubContext = hubContext;
        _currentUserService = currentUserService;
        _client = client;
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
    
    [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
    [Produces("application/json")]
    [HttpGet("{serverId:guid}/users", Name = "GetUsersOfServer")]
    public async Task<IActionResult> GetUsersOfServer(Guid serverId)
    {
        var query = new GetServerUsers.GetServerUsersQuery(serverId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [ProducesResponseType(typeof(IEnumerable<ServerDto>), 200)]
    [Produces("application/json")]
    [HttpGet("me/servers", Name = "GetServersOfUser")]
    public async Task<IActionResult> GetServersOfUser()
    {
        var query = new GetServersOfUser.GetServersOfUserQuery(_currentUserService.UserId);
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
    
    [ProducesResponseType(typeof(IEnumerable<ChannelDto>), 200)]
    [Produces("application/json")]
    [HttpGet("{serverId:guid}/channels", Name = "GetChannels")]
    public async Task<IActionResult> GetChannels(Guid serverId)
    {
        var query = new GetChannelList.ChannelListQuery(serverId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [ProducesResponseType(typeof(ServerDto), 201)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddServer")]
    public IActionResult AddServer([FromBody] CreateServerModel createServerModel)
    {
        var serverId = Guid.NewGuid();
        var server = _client.GetGrain<IServerGrain>(serverId);
        server.CreateAsync(new CreateServerCommand(serverId, createServerModel.Name, _currentUserService.UserId));
        return Ok(); // Created() // TODO Handle return //(nameof(GetServerById), new {serverId}, commandResponse);
    }
    
    [ProducesResponseType(typeof(ChannelDto), 200)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("{serverId:guid}/channels", Name = "AddServerChannel")]
    public ActionResult AddServerChannel(Guid serverId, [FromBody] CreateChannelModel channelModel)
    {
        var channelId = Guid.NewGuid();
        var channel = _client.GetGrain<IChannelGrain>(channelId);
        channel.CreateAsync(new CreateChannelCommand(serverId, channelId, channelModel.Name, channelModel.Topic,
            channelModel.Type));
        return Ok();
        //return CreatedAtAction(nameof(GetChannel), new {commandResponse.Id}, commandResponse);
    }
    
    [ProducesResponseType(204)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteServer")]
    public async Task<ActionResult> DeleteServer(Guid id)
    {
        var command = new DeleteServer.DeleteServerCommand(id, _currentUserService.UserId);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [ProducesResponseType(204)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateServer")]
    public async Task<IActionResult> UpdateServer(Guid id, CreateServerModel createServer)
    {
        var command = new UpdateServer.UpdateServerCommand(id, createServer);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [ProducesResponseType(204)]
    [HttpPost("join/{serverId:guid}", Name = "JoinServer")]
    public async Task<IActionResult> JoinServer(Guid serverId)
    {
        var command = new JoinServer.JoinServerCommand(serverId, _currentUserService.UserId);
        await _mediator.Send(command);
        
        //var joinedServer = await _mediator.Send(new GetServerById.GetServerByIdGetQuery(serverId));
        //await HubHelpers.JoinedServer(joinedServer,
        //    ControllerContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), _hubContext);
            
        //public string? UserId => _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        return NoContent();
    }
    
    [ProducesResponseType(204)]
    [HttpPost("leave/{serverId:guid}", Name = "LeaveServer")]
    public async Task<IActionResult> LeaveServer(Guid serverId)
    {
        var command = new LeaveServer.LeaveServerCommand(serverId, _currentUserService.UserId);
        await _mediator.Send(command);

        // TODO: Leave error message, if leaver is owner, tell them to delete server within server settings
        
        return NoContent();
    }
}