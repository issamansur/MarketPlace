using MarketPlace.Application.CQRS.Roles.Commands;
using MarketPlace.Application.CQRS.Roles.Queries;
using MarketPlace.Contracts.DTOs.Roles;

namespace MarketPlace.WebAPI.Controllers;

[ApiController]
[Route("api/roles")]
public class RoleController: ControllerBase
{
    private readonly IMediator _mediator;
    
    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        var result = await _mediator.Send(request.Adapt<CreateRoleCommand>());
        var response = new CreateRoleResponse(result);
        return Ok(response);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRole([FromRoute] Guid id)
    {
        var request = new GetRoleByIdRequest(id);
        var result = await _mediator.Send(request.Adapt<GetRoleByIdQuery>());
        var response = result.Adapt<GetRoleByIdResponse>();
        return Ok(response);
    }
}