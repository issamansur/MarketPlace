using MarketPlace.Application.CQRS.UserAdvertisements.Command;
using MarketPlace.Application.CQRS.UserAdvertisements.Filters;
using MarketPlace.Application.CQRS.UserAdvertisements.Queries;

namespace MarketPlace.Infrastructure.Mappings;

public class UserAdvertisementMappingProfile: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Commands
        config.NewConfig<CreateUserAdvertisementRequest, CreateUserAdvertisementCommand>()
            .ConstructUsing(src => new CreateUserAdvertisementCommand(
                src.CreatorId,
                src.Title,
                src.Description,
                src.Image != null ? src.Image.OpenReadStream() : null,
                src.Image != null ? Path.GetExtension(src.Image.FileName) : null
            ));
        
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
            .ConstructUsing(src => new UpdateUserAdvertisementCommand(
                src.Id,
                src.ChangerId,
                src.Title,
                src.Description,
                src.Image != null ? src.Image.OpenReadStream() : null,
                src.Image != null ? Path.GetExtension(src.Image.FileName) : null
            ));
        
        config.NewConfig<DeleteUserAdvertisementRequest, DeleteUserAdvertisementCommand>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.ChangerId, src => src.ChangerId);
        
        // Queries
        config.NewConfig<GetUserAdvertisementRequest, GetUserAdvertisementQuery>()
            .Map(dest => dest.UserAdvertisementId, src => src.Id);
        
        config.NewConfig<GetUserAdvertisementByNumberRequest, GetUserAdvertisementByNumberQuery>()
            .Map(dest => dest.UserAdvertisementNumber, src => src.Number);
        
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
                    src.UserAdvertisementSortType,
                    src.IsDesc
                ));
        
        config.NewConfig<IEnumerable<UserAdvertisement>, GetUserAdvertisementsByUserResponse>()
            .Map(dest => dest.UserAdvertisements, src => 
                src.Adapt<IEnumerable<GetUserAdvertisementResponse>>());
        
        config.NewConfig<SearchUserAdvertisementsRequest, GetAllUserAdvertisementsQuery>()
            .Map(dest => dest.Filter,
                src => new UserAdvertisementsFilter(
                    src.Query,
                    src.Page,
                    src.PageSize,
                    src.UserAdvertisementSortType,
                    src.IsDesc
                ));
        
        config.NewConfig<IEnumerable<UserAdvertisement>, SearchUserAdvertisementsResponse>()
            .Map(dest => dest.UserAdvertisements, src => 
                src.Adapt<IEnumerable<GetUserAdvertisementResponse>>());
    }
}