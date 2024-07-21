namespace MarketPlace.Application.CQRS.Users.Queries;

public class GetUserByIdQuery: IRequest<User>
{
    public Guid Id { get; }

    public GetUserByIdQuery(Guid id)
    {
        Id = id;
        
        var validator = new GetUserByIdQueryValidator();
        validator.ValidateAndThrow(this);
    }
}