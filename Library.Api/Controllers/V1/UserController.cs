using Application.UserManagement.Commands.ChangePassword;
using Application.UserManagement.Commands.DeleteUser;
using Application.UserManagement.Commands.UpdateUser;
using Application.UserManagement.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers.V1;

[Route("api/v1/users")]
public class UserController : BaseApiController
{
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUserResponse>> GetAsync(Guid id)
    {
        return Ok(await Mediator.Send(new GetUserRequest { UserId = id }));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UpdateAsync([FromBody] UpdateUser command)
    {
        _ = await Mediator.Send(command);

        return StatusCode(200);
    }

    [HttpPatch("change-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePassword command)
    {
        _ = await Mediator.Send(command);

        return StatusCode(200);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var command = new DeleteUser
        {
            UserId = id,
        };

        _ = await Mediator.Send(command);

        return Ok();
    }
}