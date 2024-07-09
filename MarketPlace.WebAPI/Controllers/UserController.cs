using Mapster;
using MarketPlace.Application.CQRS.Users.Commands;
using MarketPlace.Application.CQRS.Users.Queries;
using MarketPlace.Contracts.DTOs.Users;
using MediatR;

namespace MarketPlace.WebAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UserController: ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType<CreateUserResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var result = await _mediator.Send(request.Adapt<CreateUserCommand>());
        var response = new CreateUserResponse(result);
        return Ok(response);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType<GetUserResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var request = new GetUserRequest(id);
        var result = await _mediator.Send(request.Adapt<GetUserByIdQuery>());
        var response = result.Adapt<GetUserResponse>();
        return Ok(response);
    }
}