using MarketPlace.Application.CQRS.Users.Commands;
using MarketPlace.Application.CQRS.Users.Queries;

namespace MarketPlace.Infrastructure.Mappings;

public class UserMappingProfile: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Commands
        config.NewConfig<CreateUserRequest, CreateUserCommand>()
            .Map(dest => dest.RoleId, src => src.RoleId)
            .Map(dest => dest.Name, src => src.Name);

        config.NewConfig<User, CreateUserResponse>()
            .Map(dest => dest.Id, src => src.Id);
        
        // Queries
        config.NewConfig<GetUserRequest, GetUserByIdQuery>()
            .Map(dest => dest.Id, src => src.Id);
        
        config.NewConfig<User, GetUserResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.RoleId, src => src.RoleId)
            .Map(dest => dest.Name, src => src.Name);
    }
}