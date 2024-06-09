using Application.AuthorManagement.Commands.CreateAuthor;
using Application.AuthorManagement.Commands.DeleteAuthor;
using Application.AuthorManagement.Commands.UpdateAuthor;
using Application.AuthorManagement.Queries.GetAuthor;
using Application.AuthorManagement.Queries.GetAuthors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers.V1;

[Route("api/v1/authors")]
public class AuthorController : BaseApiController
{
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(GetAuthorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetAuthorsResponse>> GetAllAsync([FromQuery] string searchQuery, int? page, int? pageSize)
    {
        var request = new GetAuthorsRequest
        {
            SearchQuery = searchQuery,
            Page = page,
            PageSize = pageSize,
        };

        return Ok(await Mediator.Send(request));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetAuthorResponse>> GetAsync(Guid id)
    {
        return Ok(await Mediator.Send(new GetAuthorRequest { AuthorId = id }));
    }

    [HttpPost]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAuthor command)
    {
        _ = await Mediator.Send(command);

        return StatusCode(201);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAuthor command)
    {
        command.SetId(id);

        await Mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteAuthor
        {
            AuthorId = id,
        };

        _ = await Mediator.Send(command);

        return Ok();
    }
}