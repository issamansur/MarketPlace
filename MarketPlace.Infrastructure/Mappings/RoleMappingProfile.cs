using MarketPlace.Application.CQRS.Roles.Commands;
using MarketPlace.Application.CQRS.Roles.Queries;

namespace MarketPlace.Infrastructure.Mappings;

public class RoleMappingProfile: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Commands
        config.NewConfig<CreateRoleRequest, CreateRoleCommand>()
            .Map(dest => dest.Title, src => src.Title);
        
        config.NewConfig<Guid, CreateRoleResponse>()
            .Map(dest => dest.Id, src => src);
        
        // Queries
        config.NewConfig<GetRoleByIdRequest, GetRoleByIdQuery>()
            .Map(dest => dest.Id, src => src.Id);
        
        config.NewConfig<Role, GetRoleByIdResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title);
    }
}