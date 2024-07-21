using MarketPlace.Application.Features.AdvertisementReviews.Commands;
using MarketPlace.Application.Features.AdvertisementReviews.Queries;
using MarketPlace.Contracts.DTOs.AdvertisementReviews;

namespace MarketPlace.WebAPI.Controllers;

[ApiController]
[Route("api/reviews")]
public class AdvertisementReviewController: ControllerBase
{
    private readonly IMediator _mediator;
    
    public AdvertisementReviewController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType<CreateReviewResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAdvertisementReview([FromBody] CreateReviewRequest request)
    {
        var result = await _mediator.Send(request.Adapt<CreateAdvertisementReviewCommand>());
        var response = new CreateReviewResponse(result);
        return Ok(response);
    }
    
    [HttpGet]
    [ProducesResponseType<GetReviewsByAdvertisementResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAdvertisementReviews([FromQuery] GetReviewsByAdvertisementRequest request)
    {
        var result = await _mediator.Send(request.Adapt<GetReviewsByAdvertisementQuery>());
        var response = result.Adapt<GetReviewsByAdvertisementResponse>();
        return Ok(response);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAdvertisementReview(
        [FromRoute] Guid id, 
        [FromBody] UpdateReviewRequest request
        )
    {
        if (id != request.Id)
        {
            return BadRequest();
        }
        
        await _mediator.Send(request.Adapt<UpdateAdvertisementReviewCommand>());
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAdvertisementReview([FromRoute] Guid id)
    {
        var request = new DeleteReviewRequest(id);
        await _mediator.Send(request.Adapt<DeleteAdvertisementReviewCommand>());
        return Ok();
    }
}