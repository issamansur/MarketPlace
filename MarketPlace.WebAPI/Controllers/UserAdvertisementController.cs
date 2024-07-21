using MarketPlace.Application.Features.UserAdvertisements.Command;
using MarketPlace.Application.Features.UserAdvertisements.Queries;
using MarketPlace.Contracts.DTOs.UserAdvertisements;

namespace MarketPlace.WebAPI.Controllers;

[ApiController]
[Route("api/advertisements")]
public class UserAdvertisementController: ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserAdvertisementController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType<CreateUserAdvertisementResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUserAdvertisement([FromForm] CreateUserAdvertisementRequest request)
    {
        var result = await _mediator.Send(request.Adapt<CreateUserAdvertisementCommand>());
        var response = new CreateUserAdvertisementResponse(result);
        return Ok(response);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType<GetUserAdvertisementResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserAdvertisementById([FromRoute] Guid id)
    {
        var request = new GetUserAdvertisementRequest(id);
        var result = await _mediator.Send(request.Adapt<GetUserAdvertisementQuery>());
        var response = result.Adapt<GetUserAdvertisementResponse>();
        return Ok(response);
    }
    
    [HttpGet("{number:int}")]
    [ProducesResponseType<GetUserAdvertisementResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserAdvertisementByNumber([FromRoute] int number)
    {
        var request = new GetUserAdvertisementByNumberRequest(number);
        var result = await _mediator.Send(request.Adapt<GetUserAdvertisementByNumberQuery>());
        var response = result.Adapt<GetUserAdvertisementResponse>();
        return Ok(response);
    }
    
    // Maybe we should use [HttpPatch] instead of [HttpPut]
    // What about (non-route and body) or (route and body without id) parameters?
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUserAdvertisement(
        [FromRoute] Guid id, 
        [FromForm] UpdateUserAdvertisementRequest request
        )
    {
        if (id != request.Id)
        {
            return BadRequest();
        }
        
        await _mediator.Send(request.Adapt<UpdateUserAdvertisementCommand>());
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUserAdvertisement(
        [FromRoute] Guid id,
        [FromBody] DeleteUserAdvertisementRequest request
        )
    {
        if (id != request.Id)
        {
            return BadRequest();
        }
        
        await _mediator.Send(request.Adapt<DeleteUserAdvertisementCommand>());
        return Ok();
    }
    
    [HttpGet]
    [ProducesResponseType<GetUserAdvertisementsByUserResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserAdvertisementsByUser([FromQuery] GetUserAdvertisementsByUserRequest request)
    {
        var result = await _mediator.Send(request.Adapt<GetUserAdvertisementsByUserQuery>());
        var response = result.Adapt<GetUserAdvertisementsByUserResponse>();
        return Ok(response);
    }
    
    [HttpGet("search")]
    [ProducesResponseType<SearchUserAdvertisementsResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchUserAdvertisementsRequest([FromQuery] SearchUserAdvertisementsRequest request)
    {
        var result = await _mediator.Send(request.Adapt<GetAllUserAdvertisementsQuery>());
        var response = result.Adapt<SearchUserAdvertisementsResponse>();
        return Ok(response);
    }
}