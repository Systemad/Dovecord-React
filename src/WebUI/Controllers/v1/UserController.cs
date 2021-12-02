using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace WebUI.Controllers.v1;

[Authorize]
[ApiController]
[RequiredScope("API.Access")]
[Route("api/users")]
public class UserController : ControllerBase
{

}