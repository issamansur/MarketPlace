using MarketPlace.Domain.Entities.UserAdvertisements;

using MarketPlace.Application.UserAdvertisements.Command;
using MarketPlace.Application.UserAdvertisements.Queries;
using MarketPlace.Application.UserAdvertisements.Filters;

namespace MarketPlace.Contracts.Mappings;

public class UserAdvertisementMappingProfile: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Commands
        config.NewConfig<CreateUserAdvertisementRequest, CreateUserAdvertisementCommand>()
            .Map(dest => dest.CreatorId, src => src.CreatorId)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.ImageUrl, 
                src => src.Image != null ? src.Image.OpenReadStream() : null);
        
        config.NewConfig<UserAdvertisement, GetUserAdvertisementResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.CreatorId, src => src.CreatorId)
            .Map(dest => dest.Number, src => src.Number)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.ImageUrl, src => src.ImageUrl)
            .Map(dest => dest.Rating, src => src.Rating)
            .Map(dest => dest.DateCreated, src => src.DateCreated)
            .Map(dest => dest.DateUpdated, src => src.DateUpdated)
            .Map(dest => dest.DateExpired, src => src.DateExpired);
        
        config.NewConfig<UpdateUserAdvertisementRequest, UpdateUserAdvertisementCommand>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.ChangerId, src => src.ChangerId)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.ImageUrl, 
                src => src.Image != null ? src.Image.OpenReadStream() : null);
        
        config.NewConfig<DeleteUserAdvertisementRequest, DeleteUserAdvertisementCommand>()
            .Map(dest => dest.Id, src => src.Id);
        
        // Queries
        config.NewConfig<GetUserAdvertisementRequest, GetUserAdvertisementQuery>()
            .Map(dest => dest.UserAdvertisementId, src => src.Id);
        
        config.NewConfig<UserAdvertisement, GetUserAdvertisementResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.CreatorId, src => src.CreatorId)
            .Map(dest => dest.Number, src => src.Number)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.ImageUrl, src => src.ImageUrl)
            .Map(dest => dest.Rating, src => src.Rating)
            .Map(dest => dest.DateCreated, src => src.DateCreated)
            .Map(dest => dest.DateUpdated, src => src.DateUpdated)
            .Map(dest => dest.DateExpired, src => src.DateExpired);

        config.NewConfig<GetUserAdvertisementsByUserRequest, GetUserAdvertisementsByUserQuery>()
            .Map(dest => dest.Filter,
                src => new UserAdvertisementsByUserFilter(
                    src.UserId,
                    src.Page,
                    src.PageSize,
                    src.SortType,
                    src.IsDesc
                ));
        
        config.NewConfig<IEnumerable<UserAdvertisement>, GetUserAdvertisementsByUserResponse>()
            .Map(dest => dest.UserAdvertisements, src => 
                src.Adapt<IEnumerable<GetUserAdvertisementResponse>>());
        
        config.NewConfig<SearchUserAdvertisementsRequest, GetAllUserAdvertisementsQuery>()
            .Map(dest => dest.Filter,
                src => new UserAdvertisementsFilter(
                    src.Page,
                    src.PageSize,
                    src.SortType,
                    src.IsDesc
                ));
        
        config.NewConfig<IEnumerable<UserAdvertisement>, SearchUserAdvertisementsResponse>()
            .Map(dest => dest.UserAdvertisements, src => 
                src.Adapt<IEnumerable<GetUserAdvertisementResponse>>());
    }
}