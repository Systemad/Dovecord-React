using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebUI.Domain.Channels.Features;
using WebUI.Domain.Channels.Features;
using WebUI.Domain.Users.Features;
using WebUI.Dtos.Channel;
using WebUI.Dtos.User;

namespace WebUI.Controllers.v1;

// TODO:
//[Authorize]
[ApiController]
//[RequiredScope("API.Access")]
[Route("api/channels")]
[ApiVersion("1.0")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IMediator _mediator;
    
    public UserController(ILogger<UserController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }   
    
    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var query = new GetUserList.UserListQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}", Name = "GetUser")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var query = new GetUser.UserQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost(Name = "AddChannel")]
    public async Task<IActionResult> AddUser([FromBody] UserManipulationDto userForCreation)
    {
        var command = new AddUser.AddUserCommand(userForCreation);
        var commandResponse = await _mediator.Send(command);
        return Ok(commandResponse);
    }
    
    [HttpDelete("{id:guid}", Name = "DeleteUser")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var command = new DeleteUser.DeleteUserCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpPut("{id:guid}", Name = "UpdateUser")]
    public async Task<ActionResult> UpdateUser(Guid id, UserManipulationDto user)
    {
        var command = new UpdateUser.UpdateUserCommand(id, user);
        await _mediator.Send(command);
        return NoContent();
    }
}