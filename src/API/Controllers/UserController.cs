using Application.Channels.Features;
using Application.Users.Features;
using Domain.Channels.Dto;
using Domain.Users.Dto;
using Dovecord.Extensions.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Orleans;

namespace Dovecord.Controllers;

[Authorize]
[ApiController]
[RequiredScope("API.Access")]
[Route("api/v{version:apiVersion}/users")]
[ApiVersion("1.0")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    private readonly IClusterClient _client;
    
    public UserController(ILogger<UserController> logger, IMediator mediator, ICurrentUserService currentUserService, IClusterClient client)
    {
        _logger = logger;
        _mediator = mediator;
        _currentUserService = currentUserService;
        _client = client;
    }   
    
    [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
    [Produces("application/json")]
    [HttpGet("me", Name = "GetMe")]
    public async Task<IActionResult> GetMe()
    {
        var query = new GetMe.GetMeQuery(_currentUserService.UserId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
    [Produces("application/json")]
    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var query = new GetUserList.UserListQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [ProducesResponseType(typeof(UserDto), 200)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetUser")]
    public async Task<ActionResult> GetUser(Guid id)
    {
        var query = new GetUser.GetUserQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [Obsolete]
    [ProducesResponseType(typeof(UserDto), 201)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddUser")]
    public async Task<ActionResult> AddUser([FromBody] UserCreationDto userForCreation)
    {
        //var command = new AddUser.AddUserCommand(userForCreation);
        //var commandResponse = await _mediator.Send(command);
        return Ok();
    }
    
    [ProducesResponseType(204)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteUser")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        var command = new DeleteUser.DeleteUserCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [ProducesResponseType(204)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateUser")]
    public async Task<IActionResult> UpdateUser(Guid id, UserManipulationDto user)
    {
        //var command = new UpdateUser.UpdateUserCommand(id, user);
        //await _mediator.Send(command);
        return NoContent();
    }
    
    [ProducesResponseType(typeof(ChannelDto), 200)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("me/channels", Name = "AddUserChannel")]
    public async Task<IActionResult> AddUserChannel([FromBody]Guid recipientId)
    {
        //var command = new AddUserChannel.AddUserChannelCommand(recipientId, _currentUserService.UserId);
        //var commandResponse = await _mediator.Send(command);
        return Ok();
        //return CreatedAtAction(nameof(GetChannel), new {commandResponse.Id}, commandResponse);
    }
    
    [ProducesResponseType(200)]
    [Produces("application/json")]
    [HttpPut("me/setting", Name = "UpdateUserSettings")]
    public async Task<IActionResult> UpdateUserSettings(UserSettingsManipulationDto user)
    {
        //var command = new UpdateUserSettings.UpdateUserSettingsCommand(_currentUserService.UserId, user);
        //var updatesSettings = await _mediator.Send(command);
        return Ok();
    }
}