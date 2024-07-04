using MarketPlace.Application.CQRS.AdvertisementReviews.Commands;
using MarketPlace.Application.CQRS.AdvertisementReviews.Queries;
using MarketPlace.Contracts.DTOs.AdvertisementReviews;

namespace MarketPlace.WebAPI.Controllers;

[ApiController]
[Route("api/advertisement_reviews")]
public class AdvertisementReviewController: ControllerBase
{
    private readonly IMediator _mediator;
    
    public AdvertisementReviewController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAdvertisementReview([FromBody] CreateReviewRequest request)
    {
        var result = await _mediator.Send(request.Adapt<CreateAdvertisementReviewCommand>());
        var response = new CreateReviewResponse(result);
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAdvertisementReviews([FromQuery] GetReviewsByAdvertisementRequest request)
    {
        var result = await _mediator.Send(request.Adapt<GetReviewsByAdvertisementQuery>());
        var response = result.Adapt<GetReviewsByAdvertisementResponse>();
        return Ok(response);
    }
    
    [HttpPut("{id:guid}")]
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
    public async Task<IActionResult> DeleteAdvertisementReview([FromRoute] Guid id)
    {
        var request = new DeleteReviewRequest(id);
        await _mediator.Send(request.Adapt<DeleteAdvertisementReviewCommand>());
        return Ok();
    }
}