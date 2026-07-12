namespace BakeAndCake.Api.Models;

public record CustomerModel(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    string? Address,
    bool IsLoyaltyMember,
    int LoyaltyPoints,
    DateTime CreatedAt
);

public record CreateCustomerModel(
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    string? Address,
    bool IsLoyaltyMember
);

public record UpdateCustomerModel(
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    string? Address,
    bool IsLoyaltyMember
);

public record AdjustLoyaltyPointsModel(int Points);   // positive = earn, negative = redeem
