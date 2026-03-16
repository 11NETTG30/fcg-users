using MassTransit;

namespace FCG.Users.Infrastructure.Messaging.Configurations;

public class CustomNameEntityNameFormatter : IEntityNameFormatter
{
    public string FormatEntityName<T>()
    {
        return typeof(T).Name;
    }
}