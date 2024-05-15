namespace MarketPlace.Contracts.DTOs.Users;

public record GetUserResponse(
    Guid Id,
    Guid RoleId,
    string Name
    );