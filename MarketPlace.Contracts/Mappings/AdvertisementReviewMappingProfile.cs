using MarketPlace.Domain.Entities.UserAdvertisements;

using MarketPlace.Application.AdvertisementReviews.Commands;
using MarketPlace.Application.AdvertisementReviews.Filters;
using MarketPlace.Application.AdvertisementReviews.Queries;


namespace MarketPlace.Contracts.Mappings;

public class AdvertisementReviewMappingProfile: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Commands
        config.NewConfig<CreateReviewRequest, CreateAdvertisementReviewCommand>()
            .Map(dest => dest.AdvertisementId, src => src.AdvertisementId)
            .Map(dest => dest.CreatorId, src => src.CreatorId)
            .Map(dest => dest.Rating, src => src.Rating)
            .Map(dest => dest.Comment, src => src.Comment);
        
        config.NewConfig<AdvertisementReview, CreateReviewResponse>()
            .Map(dest => dest.Id, src => src.Id);
        
        config.NewConfig<UpdateReviewRequest, UpdateAdvertisementReviewCommand>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Rating, src => src.Rating)
            .Map(dest => dest.Comment, src => src.Comment);
        
        // Queries
        config.NewConfig<AdvertisementReview, GetReviewResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.AdvertisementId, src => src.AdvertisementId)
            .Map(dest => dest.CreatorId, src => src.CreatorId)
            .Map(dest => dest.Rating, src => src.Rating)
            .Map(dest => dest.Comment, src => src.Comment)
            .Map(dest => dest.DateCreated, src => src.DateCreated);
        
        config.NewConfig<GetReviewsByAdvertisementRequest, GetReviewsByAdvertisementQuery>()
            .Map(dest => dest.Filter,
                src => new AdvertisementReviewsFilter(
                    src.AdvertisementId, 
                    src.Page, 
                    src.PageSize, 
                    src.SortType,
                    src.IsDesc
                    ));
        
        config.NewConfig<IEnumerable<AdvertisementReview>, GetReviewsByAdvertisementResponse>()
            .Map(dest => dest.Reviews, src => src.Adapt<IEnumerable<GetReviewResponse>>());
    }
}