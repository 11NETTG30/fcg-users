namespace FCG.Users.Infrastructure.Messaging;

using FCG.Users.Application.Abstractions.Messaging;
using MassTransit;

public class EventPublisherMassTransit(IPublishEndpoint publishEndpoint) : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        return _publishEndpoint.Publish(message, cancellationToken);
    }
}