namespace MarketPlace.Contracts.DTOs.Users;

public record CreateUserRequest(
    Guid RoleId,
    string Name
    );