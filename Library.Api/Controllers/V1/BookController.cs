using Application.BookManagement.Commands.BringInBook;
using Application.BookManagement.Commands.CreateBook;
using Application.BookManagement.Commands.DeleteBook;
using Application.BookManagement.Commands.TakeOutBook;
using Application.BookManagement.Commands.UpdateBook;
using Application.BookManagement.Queries.GetBook;
using Application.BookManagement.Queries.GetBooks;
using Domain.BookManagement.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers.V1;

[Route("api/v1/books")]
public class BookController : BaseApiController
{
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(GetBookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetBooksResponse>> GetAllAsync([FromQuery] string searchQuery, BookStatus? status, int? page, int? pageSize)
    {
        var request = new GetBooksRequest
        {
            SearchQuery = searchQuery,
            Status = status,
            Page = page,
            PageSize = pageSize,
        };

        return Ok(await Mediator.Send(request));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetBookResponse>> GetAsync(Guid id)
    {
        return Ok(await Mediator.Send(new GetBookRequest { BookId = id }));
    }

    [HttpPost]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateBook command)
    {
        _ = await Mediator.Send(command);

        return StatusCode(201);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBook command)
    {
        command.SetId(id);

        await Mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteBook
        {
            BookId = id,
        };

        _ = await Mediator.Send(command);

        return Ok();
    }

    [HttpPatch("{id}/bring-in")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> BringInAsync([FromRoute] Guid id)
    {
        var command = new BringInBook
        {
            BookId = id,
        };

        _ = await Mediator.Send(command);

        return StatusCode(200);
    }

    [HttpPatch("{id}/take-out")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> TakeOutAsync([FromRoute] Guid id)
    {
        var command = new TakeOutBook
        {
            BookId = id,
        };

        _ = await Mediator.Send(command);

        return StatusCode(200);
    }
}