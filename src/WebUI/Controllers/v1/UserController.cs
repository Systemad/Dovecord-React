using Infrastructure.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using WebUI.Domain.Users.Features;
using WebUI.Dtos.User;

namespace WebUI.Controllers.v1;

[Authorize]
[ApiController]
[RequiredScope("API.Access")]
[Route("api/users")]
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
        var query = new GetUser.UserQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [ProducesResponseType(typeof(UserDto), 201)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddUser")]
    public async Task<ActionResult> AddUser([FromBody] UserManipulationDto userForCreation)
    {
        var command = new AddUser.AddUserCommand(userForCreation);
        var commandResponse = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUser), new {commandResponse.Id}, commandResponse);
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
        var command = new UpdateUser.UpdateUserCommand(id, user);
        await _mediator.Send(command);
        return NoContent();
    }
}