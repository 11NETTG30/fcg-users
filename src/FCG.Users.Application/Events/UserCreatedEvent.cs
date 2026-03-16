namespace FCG.Users.Application.Events;

public sealed record UserCreatedEvent(Guid UserId, string Email);
