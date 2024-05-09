namespace MarketPlace.Application.Roles.Queries;

public class GetRoleByIdQuery: IRequest<Role>
{
    public Guid Id { get; }

    public GetRoleByIdQuery(Guid id)
    {
        Id = id;
    }
}