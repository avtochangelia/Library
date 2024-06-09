using Application.UserManagement.Commands.RegisterUser;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers.V1;

[Route("api/v1/account")]
public class AccountController : BaseApiController
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUser request)
    {
        _ = await Mediator.Send(request);

        return StatusCode(201);
    }
}